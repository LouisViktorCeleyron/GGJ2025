using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _engageTransform;
    [SerializeField]
    private Rigidbody _rigidbody; 
    [SerializeField]
    private float _power=100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Engage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Engage();
        }
    }

    private void Engage()
    {
        transform.position = _engageTransform.position; 
        var leftOrRight = Mathf.Sign(Random.Range(-1, 1));
        Bounce(Vector3.right * leftOrRight + Vector3.up);
    }

    public void Bounce(Vector3 direction)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(direction * _power);
    }

}
