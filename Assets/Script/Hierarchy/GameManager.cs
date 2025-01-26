using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public Transform engageTransform;
    [HideInInspector]
    public BubbleReferencer bubbleReference;

    [SerializeField]
    private Character _leftChar, _rightChar;


    [SerializeField]
    private float _netTolerance = .3f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            StartCoroutine(WaitForLaunch());
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void LaunchEngage(bool waitTime = true)
    {
        CountPoint();
        bubbleReference.transform.position = engageTransform.position;
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
        var leftOrRight = -1;
        bubbleReference.BubbleMovement.Bounce(Vector3.right * leftOrRight + Vector3.up);
    }

    private bool IsSetUp()
    {
        return bubbleReference != null && engageTransform !=null;
    }


    public IEnumerator DelayedAction(UnityAction action, float f = 1)
    {
        yield return new WaitForSeconds(f);
        action.Invoke();
    }
    private IEnumerator WaitForLaunch()
    {
        yield return new WaitUntil(IsSetUp);
        Engage();
    }

    private void CountPoint()
    {
        var bPos = bubbleReference.transform.position;
        var netMin = engageTransform.position.x - _netTolerance;
        var netMax = engageTransform.position.x + _netTolerance;
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
