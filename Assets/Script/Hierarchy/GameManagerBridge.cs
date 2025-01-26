using UnityEngine;

public class GameManagerBridge : VolleyBulleGO
{

    [SerializeField]
    private Transform _engageTransformL, _engageTransformR;


    void Start()
    {
        _GameManager.engageTransformL = _engageTransformL;
        _GameManager.engageTransformR = _engageTransformR;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
