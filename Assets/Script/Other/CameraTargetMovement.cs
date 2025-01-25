using UnityEngine;

public class CameraTargetMovement : VolleyBulleGO
{
    [SerializeField]
    private float _minMax = 3.5f;

    private Transform _ballTransform;

    void Start()
    {
        _ballTransform = _GameManager.bubbleReference.transform;
    }

    void Update()
    {
        var x = Mathf.Clamp(_ballTransform.position.x,-_minMax,_minMax);
        transform.position = transform.position.y*Vector3.up + Vector3.right*x;
    }
}
