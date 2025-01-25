using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Transform _engageTransform;
    private BubbleMovement _bubbleReference;
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

    public void Engage()
    {
        transform.position = _engageTransform.position;
        var leftOrRight = -1;
        _bubbleReference.Bounce(Vector3.right * leftOrRight + Vector3.up);
    }

    private bool IsSetUp()
    {
        return _bubbleReference != null && _engageTransform !=null;
    }


    private IEnumerator WaitForLaunch()
    {
        yield return new WaitUntil(IsSetUp);
        Engage();
    }

}
