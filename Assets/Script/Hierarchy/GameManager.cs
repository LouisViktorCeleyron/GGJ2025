using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public Transform engageTransformL, engageTransformR;
    [HideInInspector]
    public BubbleReferencer bubbleReference;

    public Character leftChar, rightChar;
    [SerializeField]
    private CharacterData _dataLeft, _dataRight, _winner;
    public CharacterData LeftChar => _dataLeft;
    public CharacterData RightChar => _dataRight;
    public CharacterData Winner => _winner;
    public bool LeftSelected => _dataLeft != null;
    public bool RightSelected => _dataRight != null;

    [HideInInspector]
    public TextMeshProUGUI timerText;

    [SerializeField]
    private float _netTolerance = .3f, _timerMax = 120;
    private float _timer=30;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void ResetGM()
    {
        leftChar = null;
        rightChar = null;  
        _dataLeft = null;
        _dataRight = null;
        _winner = null;
        _timer = _timerMax;
    }

    public void SetPlayer(CharacterData data, bool playerTwo)
    {
        if(playerTwo)
        {
            _dataLeft = data;
        }
        else
        {
            _dataRight = data;
        }
        if(_dataLeft != null && _dataRight!= null) 
        {
            SceneManager.LoadScene(2);
        }
    }
    public void StartGameplay()
    {
        StartCoroutine(WaitForLaunch());
        leftChar.Initialize(_dataLeft);
        rightChar.Initialize(_dataRight);

    }

    private bool _isLeftPlayerEngage = false;
    public void LaunchEngage(bool waitTime = true, bool countPoint  = true)
    {
        if(_timer<=0)
        {
            return;
        }
        if(countPoint)
        {
            CountPoint();
        }
        _isLeftPlayerEngage = Random.Range(0f, 1f) > .5f;
        bubbleReference.transform.position = _isLeftPlayerEngage? engageTransformL.position : engageTransformR.position;
        bubbleReference.Rigidbody.useGravity = false;
        bubbleReference.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        
        var wt = waitTime ? 1:0;
        StartCoroutine(DelayedAction(Engage, wt));
    }
    private void Engage()
    {
        bubbleReference.BulleLife.ResetHp();
        bubbleReference.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        bubbleReference.Rigidbody.useGravity = true;

        var dirR = Vector3.right + Vector3.up + Vector3.back;
        var dirL = Vector3.left+ Vector3.up + Vector3.forward;

        bubbleReference.BubbleMovement.Bounce(_isLeftPlayerEngage?dirL:dirR);
    }

    private bool IsSetUp()
    {
        return bubbleReference != null && engageTransformL !=null && engageTransformR != null && rightChar != null && leftChar != null;
    }

    public IEnumerator Timer()
    {
        while(_timer>=0)
        {
            yield return new WaitForSeconds(1);
            _timer -= 1;
            timerText.text = _timer.ToString("000");
            if(_timer<0)
            {
                SetWinner();
            }
        }
    }

    private void SetWinner()
    {
        StopAllCoroutines();
        if(leftChar.Score>rightChar.Score)
        {
            _winner = _dataLeft;
        }
        if (leftChar.Score < rightChar.Score)
        {
            _winner = _dataRight;
        }
        SceneManager.LoadScene(3);

    }
    public IEnumerator DelayedAction(UnityAction action, float f = 1)
    {
        yield return new WaitForSeconds(f);
        action.Invoke();
    }
    private IEnumerator WaitForLaunch()
    {
        yield return new WaitUntil(IsSetUp);
        LaunchEngage(true, false);
        timerText.text = _timerMax.ToString("000");
        StartCoroutine(Timer()); 

    }

    private void CountPoint()
    {
        var bPos = bubbleReference.transform.position;
        var netMin = engageTransformL.position.x - _netTolerance;
        var netMax = engageTransformR.position.x + _netTolerance;
        
        if(bPos.x < netMin)
        {
            rightChar.RiseScore(1);
            return;
        }
        if (bPos.x > netMax)
        {
            leftChar.RiseScore(1);
            return ;
        }

        //equality

    }
}
