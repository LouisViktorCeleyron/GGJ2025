using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : VolleyBulleGO
{
    [Header("Refs")]
    [SerializeField]
    private Rigidbody _rgbd;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private CharacterData _characterData;

    public void Initialize(CharacterData data)
    {
        _characterData = data;
        SpriteRenderer spriteRenderer = _spriteRenderer;
        ChangeScore(0);
    }

    [Header("Movement")]
    [SerializeField] 
    private float _speed, _xBoost=1.5f, _zBoost=.5f, _yBoost = 2f;
    private Vector3 _velocity;
    private float _hAxis, _vAxis;


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Blow();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isBlowing = false;
        }
        if (_blowingJauge < 1 && !_isBlowing)
        {
            UnBlow();
        }

    }

    private void FixedUpdate()
    {
        _velocity.x = _hAxis = Input.GetAxisRaw("Horizontal");
        _velocity.z = _vAxis = Input.GetAxisRaw("Vertical");

        _velocity = _velocity.normalized * _speed * Time.fixedDeltaTime;

        _rgbd.MovePosition(_rgbd.position + _velocity);
    }


    [Header("Blow")]
    private bool _isBlowing;
    private float _blowingJauge;
    
    [SerializeField]
    private UnityEvent _onBlowFrame,_onUnblowFrame;

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


    [Header("Score")]
    private int _score;
    [SerializeField]
    private UnityEvent<int> _onScoreChanged;
    public void RiseScore(int amount)
    {
        ChangeScore(_score+amount);
    }
    public void ChangeScore(int score)
    {
        _score = score;
        _onScoreChanged.Invoke(score);
    }
}
