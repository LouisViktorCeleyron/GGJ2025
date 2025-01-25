using UnityEngine;

public class BubbleReferencer : VolleyBulleGO
{
    [SerializeField]
    private BubbleMovement _bubbleMovement;
    public BubbleMovement BubbleMovement => _bubbleMovement;
    [SerializeField]
    private BubbleLife _bubbleLife;
    public BubbleLife BulleLife => _bubbleLife;

    [SerializeField]
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;

    void Start()
    {
        _GameManager.bubbleReference = this;
    }
}
