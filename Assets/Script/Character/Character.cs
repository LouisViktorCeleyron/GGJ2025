using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : VolleyBulleGO
{
    [SerializeField]
    private Rigidbody _rgbd;

    [SerializeField] 
    private float _speed, _xBoost=1.5f, _zBoost=.5f, _yBoost = 2f;
    private Vector3 _velocity;

    private float _hAxis, _vAxis;

    private bool _isBlowing;
    private float _blowingJauge;
    
    [SerializeField]
    private UnityEvent _onBlowFrame,_onUnblowFrame;


    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Blow();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _isBlowing = false;
        }
        if (_blowingJauge < 1)
        {
            UnBlow();
        }

    }

    private void Blow()
    {
        _isBlowing = true;
        _blowingJauge -= 0.1f;
        _blowingJauge = Mathf.Clamp01(_blowingJauge);
        _onBlowFrame.Invoke();
    }

    private void UnBlow()
    {
        _blowingJauge += 0.1f;
        _blowingJauge = Mathf.Clamp01(_blowingJauge);
        _onUnblowFrame.Invoke();
    }

    private void FixedUpdate()
    {
        _velocity.x = _hAxis = Input.GetAxisRaw("Horizontal");
        _velocity.z = _vAxis = Input.GetAxisRaw("Vertical");

        _velocity = _velocity.normalized*_speed*Time.fixedDeltaTime;

        _rgbd.MovePosition(_rgbd.position + _velocity);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        var bubbleRef = collision.gameObject.GetComponent<BubbleReferencer>();
        
        if(bubbleRef != null)
        {
            var bubbleCollision = bubbleRef.BubbleMovement;
            if(!bubbleRef.BulleLife.IsDead)
            {
                var frwrdV = Vector3.forward * Mathf.Abs(_vAxis)* _zBoost *Mathf.Sign(_vAxis);
                var rightV = Vector3.right * Mathf.Max(1, _hAxis * _xBoost);
                bubbleCollision.Bounce(rightV+frwrdV+Vector3.up*_yBoost);
            }
            
            if(!_isBlowing)
            {
                bubbleRef.BulleLife.ReduceHp(1);
            }

        }
    }


}
