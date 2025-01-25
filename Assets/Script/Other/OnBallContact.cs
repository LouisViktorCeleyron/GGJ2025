using UnityEngine;

public class OnBallContact : VolleyBulleGO
{
    [SerializeField]
    private bool _damage, _destroy;
    

    private void OnCollisionEnter(Collision collision)
    {
        var bubbleCollision = collision.gameObject.GetComponent<BubbleMovement>();
        if (bubbleCollision != null)
        {

        }
    }

    public void DestroyBall(BubbleMovement bubble)
    {
        _GameManager.Engage();
    }
}
