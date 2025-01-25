using UnityEngine;

public class OnBubbleContact : VolleyBulleGO
{
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private Vector3 bounciness;

    private void OnCollisionEnter(Collision collision)
    {
        var bubble = collision.gameObject.GetComponent<BubbleReferencer>();
        if (bubble != null)
        {
            var isDead = bubble.BulleLife.ReduceHp(_damage); 
            bubble.BubbleMovement.Bounce(bounciness,!isDead);
        }
    }
}
