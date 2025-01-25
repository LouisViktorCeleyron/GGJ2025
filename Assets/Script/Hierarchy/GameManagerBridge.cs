using UnityEngine;

public class GameManagerBridge : VolleyBulleGO
{

    [SerializeField]
    private Transform _engageTransform;


    void Start()
    {
        _GameManager.engageTransform = _engageTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
