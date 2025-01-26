using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : VolleyBulleGO
{
    [SerializeField]
    private bool _player2;
    [SerializeField]
    private KeyCode _launch;
    private string _hAxName, _vAxName;
    [Header("Refs")]
    [SerializeField]
    private Rigidbody _rgbd;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private CharacterData _characterData;
    [SerializeField]
    private UnityEvent <CharacterData>_initEvent;
    [SerializeField]
    private UnityEvent<float> _onBreathChange;

    public void Initialize(CharacterData data)
    {
        _characterData = data;
        SpriteRenderer spriteRenderer = _spriteRenderer;
        _initEvent.Invoke(data);
        ChangeScore(0);
    }

    [Header("Movement")]
    [SerializeField] 
    private float _speed, _xBoost=1.5f, _zBoost=.5f, _yBoost = 2f;
    private Vector3 _velocity;
    private float _hAxis, _vAxis;

    private void Awake()
    {
        _hAxName = _player2 ? "Horizontal2" : "Horizontal";
        _vAxName = _player2 ? "Vertical2" : "Vertical";
    }

    void Update()
    {
        if (Input.GetKey(_launch))
        {
            Blow();
        }
        if (Input.GetKeyUp(_launch))
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
        _velocity.x = _hAxis = Input.GetAxisRaw(_hAxName);
        _velocity.z = _vAxis = Input.GetAxisRaw(_vAxName);

        _velocity = _velocity.normalized * _speed * Time.fixedDeltaTime;

        _rgbd.MovePosition(_rgbd.position + _velocity);
    }


    [Header("Blow")]
    private bool _isBlowing;
    private float _blowingJauge;
    
    

    private void Blow()
    {
        _isBlowing = true;
        UpdateBlow(.1f);
        _spriteRenderer.sprite = _characterData.spriteBlow;
    }

    private void UnBlow()
    {
        UpdateBlow(.05f);
        _spriteRenderer.sprite = _characterData.spriteIdle;
    }

    private void UpdateBlow(float amount)
    {
        _blowingJauge += amount;
        _blowingJauge = Mathf.Clamp01(_blowingJauge);
        _onBreathChange.Invoke(_blowingJauge);
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
                var rightV = Vector3.right * Mathf.Max(1, _hAxis * _xBoost)*Mathf.Sign(_xBoost);
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
