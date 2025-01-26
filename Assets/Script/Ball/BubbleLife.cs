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
    private float _invincibilityFrames = 1;

    public bool IsDead => _hp <= 0;

    public void ResetHp()
    {
        _spriteRenderer.color = Color.white;
        StopAllCoroutines();
        _isInvincible = false;
        SetHP(_hpBase);
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
        StartCoroutine(InvincibilityFrames());
        return SetHP(_hp-amount);
    }



    public void Pop()
    {
        _GameManager.LaunchEngage();
    }
}
