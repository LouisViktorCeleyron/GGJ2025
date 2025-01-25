using UnityEngine;

public class BubbleLife : VolleyBulleGO
{
    [SerializeField]
    private int _hp;
    [SerializeField]
    private BubbleReferencer _referencer;


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

    public bool ReduceHp(int amount)
    {
        return SetHP(_hp-amount);
    }


    public void Pop()
    {
        _GameManager.LaunchEngage();
    }
}
