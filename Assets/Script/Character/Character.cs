using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : VolleyBulleGO
{
    [SerializeField]
    private bool _player2;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private KeyCode _launch;
    private string _buttonName;
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
        _buttonName = _player2?"Fire1":"Fire2";
        _hAxName = _player2 ? "Horizontal2" : "Horizontal";
        _vAxName = _player2 ? "Vertical2" : "Vertical";
        if(_player2 ) 
        {
            _GameManager.leftChar = this;
        }
        else
        {
            _GameManager.rightChar = this;
        }
    }


    private void FixedUpdate()
    {
        _velocity.x = _hAxis = Input.GetAxisRaw(_hAxName);
        _velocity.z = _vAxis = Input.GetAxisRaw(_vAxName);
        
        _animator.SetBool("IsWalking",_velocity.magnitude > 0);

        _velocity = _velocity.normalized * _speed * Time.fixedDeltaTime;
        

        _rgbd.MovePosition(_rgbd.position + _velocity);
    }


    [Header("Blow")]
    private bool _isBlowing, _recoverBlow;
    private float _breath = 1;


    void Update()
    {
        if (Input.GetButton(_buttonName))
        {
            Blow();
        }
        if (Input.GetButtonUp(_buttonName))
        {
            _isBlowing = false;
            _recoverBlow = false;
        }
        if (!_isBlowing)
        {
            UnBlow();
        }
    }

    private void Blow()
    {
        if(_breath<=0)
        {
            _isBlowing=false;
            _recoverBlow = true;
            return;
        }
        if(_recoverBlow && _breath<=.5f)
        {
            return; 
        }
        _recoverBlow =false;
        _isBlowing = true;
        UpdateBlow(-2);
        _spriteRenderer.sprite = _characterData.spriteBlow;
    }

    private void UnBlow()
    {
        UpdateBlow(1);
        _spriteRenderer.sprite = _characterData.spriteIdle;
    }

    private void UpdateBlow(float amount)
    {
        _breath += amount*Time.deltaTime;
        _breath = Mathf.Clamp01(_breath);
        _onBreathChange.Invoke(_breath);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        var bubbleRef = collision.gameObject.GetComponent<BubbleReferencer>();
        
        if(bubbleRef != null)
        {
            var bubbleCollision = bubbleRef.BubbleMovement;
            if(!bubbleRef.BulleLife.IsDead)
            {
                var frwrdV = Vector3.forward * (Mathf.Abs(_vAxis)* _zBoost *Mathf.Sign(_vAxis)+ Random.Range(-.25f, .25f));
                var rightV = Vector3.right * Mathf.Max(1, _hAxis * _xBoost)*Mathf.Sign(_xBoost);
                var upBonus = (_isBlowing ? 1f:1.25f) + (bubbleCollision.isGrounded?.5f:0);
                bubbleCollision.Bounce(rightV+frwrdV+Vector3.up*upBonus*_yBoost);
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
