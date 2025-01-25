using UnityEngine;

public class BubbleMovement : VolleyBulleGO
{
    [SerializeField]
    private Rigidbody _rigidbody; 
    [SerializeField]
    private float _power=100;

    public void Bounce(Vector3 direction)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(direction * _power);
    }

}
