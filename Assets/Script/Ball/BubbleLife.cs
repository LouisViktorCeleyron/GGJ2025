using System.Collections;
using UnityEngine;

public class BubbleLife : VolleyBulleGO
{
    [SerializeField]
    private int _hp;
    [SerializeField]
    private BubbleReferencer _referencer;
    
    [SerializeField]
    private int _hpBase;
    [SerializeField]
    private bool _isInvincible;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private float _invincibilityFrames = 1;

    public bool IsDead => _hp <= 0;

    public void ResetHp()
    {
        _spriteRenderer.color = Color.white;
        StopAllCoroutines();
        _isInvincible = false;
        SetHP(Random.Range(_hpBase, _hpBase+3));
    }

    public bool SetHP(int hp)
    {
        _hp = hp;
        if(hp <= 0)
        {
            Pop();
            return true;
        }
        return false;
    }
    private IEnumerator LaunchReset()
    {
        yield return new WaitForSeconds(1);
        _particleSystem.Clear();
        _particleSystem.Stop();
        _GameManager.LaunchEngage();
        _spriteRenderer.enabled = true;
    }
    private IEnumerator InvincibilityFrames()
    {
        _isInvincible = true;
        for (float i = 0f; i < _invincibilityFrames; i+=.1f)
        {
            _spriteRenderer.color = i%.2f==0 ? Color.red : Color.white;
            yield return new WaitForSeconds(.1f);
        }
        _spriteRenderer.color = Color.white ;
        _isInvincible = false;
    }

    public bool ReduceHp(int amount)
    {
        if (_isInvincible)
        {
            return false;
        }
        var b = SetHP(_hp - amount);
        if(!b) 
        {
            StartCoroutine(InvincibilityFrames());
        }
        return b;
    }



    public void Pop()
    {   
        _particleSystem.Play ();
        _spriteRenderer.enabled = false;
        StartCoroutine(LaunchReset());
    }
}
