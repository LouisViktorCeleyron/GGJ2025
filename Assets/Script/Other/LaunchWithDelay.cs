using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LaunchWithDelay : MonoBehaviour
{

    public UnityEvent toLaunch;
    public float delayMin=.5f,delayMax=2f;
    public bool loop;

    void Start()
    {
        
    }

    private IEnumerator Launch()
    {
        while (loop)
        {
            var d = Random.Range(delayMin,delayMax);
            yield return new WaitForSeconds(d);
            toLaunch.Invoke();
        }
    }
    
}
