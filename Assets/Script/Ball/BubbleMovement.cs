using UnityEngine;

public class BubbleMovement : VolleyBulleGO
{
    [SerializeField]
    private Rigidbody _rigidbody; 
    [SerializeField]
    private float _power=100;
    [SerializeField]
    private LayerMask _layerMask;
    public bool isGrounded,isNextToWall;
    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out var h,  1.1f, _layerMask);
        isNextToWall = Physics.Raycast(transform.position + Vector3.left * 1.1f, Vector3.right, out var i,2.2f, _layerMask) ;
    }

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
