using Mono.Cecil.Cil;
using UnityEngine;

public class BubbleMovement : VolleyBulleGO
{
    [SerializeField]
    private Rigidbody _rigidbody; 
    [SerializeField]
    private float _power=100;

    public void Bounce(Vector3 direction, bool removeOnlyYVelocity = false)
    {
        var tempVector = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        _rigidbody.linearVelocity = Vector3.zero;
        if(removeOnlyYVelocity)
        {
            _rigidbody.AddForce((tempVector.normalized+direction)*_power);
        }
        else
        {
            _rigidbody.AddForce(direction * _power);
        }
    }

}
