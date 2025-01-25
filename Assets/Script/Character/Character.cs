using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : VolleyBulleGO
{
    [SerializeField]
    private Rigidbody _rgbd;

    [SerializeField] 
    private float _speed, _xBoost=1.5f, _zBoost=.5f, _yBoost = 2f;
    private Vector3 _velocity;

    private float _hAxis, _vAxis;

    void Update()
    {
    }

    private void FixedUpdate()
    {
        _velocity.x = _hAxis = Input.GetAxisRaw("Horizontal");
        _velocity.z = _vAxis = Input.GetAxisRaw("Vertical");

        _velocity = _velocity.normalized*_speed*Time.fixedDeltaTime;

        Debug.Log(_velocity);

        _rgbd.MovePosition(_rgbd.position + _velocity);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        var bubbleCollision = collision.gameObject.GetComponent<BubbleMovement>();
        if(bubbleCollision != null)
        {
            var frwrdV = Vector3.forward * Mathf.Abs(_vAxis)* _zBoost *Mathf.Sign(_vAxis);
            var rightV = Vector3.right * Mathf.Max(1, _hAxis * _xBoost);
            bubbleCollision.Bounce(rightV+frwrdV+Vector3.up*_yBoost);
        }
    }
}
