using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public Transform engageTransformL, engageTransformR;
    [HideInInspector]
    public BubbleReferencer bubbleReference;

    [SerializeField]
    private Character _leftChar, _rightChar;
    [SerializeField]
    private CharacterData _dataLeft, _dataRight;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private float _netTolerance = .3f, _timer = 120;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            StartCoroutine(WaitForLaunch());
            _leftChar.Initialize(_dataLeft);
            _rightChar.Initialize(_dataRight);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private bool _isLeftPlayerEngage = false;
    public void LaunchEngage(bool waitTime = true, bool countPoint  = true)
    {
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
        return bubbleReference != null && engageTransformL !=null && engageTransformR != null;
    }

    public IEnumerator Timer()
    {
        while(_timer>=0)
        {
            yield return new WaitForSeconds(1);
            _timer -= 1;
            _timerText.text = _timer.ToString("000");
        }
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
        _timerText.text = _timer.ToString("000");
        StartCoroutine(Timer()); 

    }

    private void CountPoint()
    {
        var bPos = bubbleReference.transform.position;
        var netMin = engageTransformL.position.x - _netTolerance;
        var netMax = engageTransformR.position.x + _netTolerance;
        
        if(bPos.x < netMin)
        {
            _rightChar.RiseScore(1);
            return;
        }
        if (bPos.x > netMax)
        {
            _leftChar.RiseScore(1);
            return ;
        }

        //equality

    }
}
