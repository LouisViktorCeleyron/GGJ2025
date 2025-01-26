using UnityEngine;

public class ShadowFollow : VolleyBulleGO
{
    void Update()
    {
        var bPos = _GameManager.bubbleReference.transform.position;
        transform.position = new Vector3(bPos.x, transform.position.y, bPos.z);     
    }
}
