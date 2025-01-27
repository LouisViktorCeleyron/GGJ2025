using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Crabistouille))]
public class CrabistouilleEditor : Editor
{
    private Crabistouille crbst;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("CRABISOTUILLE"))
        {
            crbst = (Crabistouille)target;
            CrabistouilleUnleash();
        }
    }

    public void CrabistouilleUnleash()
    {
        var i = Instantiate(crbst.prefab,crbst.transform.position,crbst.transform.rotation);
        var sr = i.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = crbst.GetComponent<SpriteRenderer>().sprite;
        i.transform.localScale = crbst.transform.localScale;
        DestroyImmediate(crbst.gameObject);
    }
}
