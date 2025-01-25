using UnityEngine;

public class DebugManager : VolleyBulleGO
{
    private bool _isDebug;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _isDebug = !_isDebug;
        }
    }

    private void OnGUI()
    {
        if (!_isDebug)
        {
            return;
        }

        if (GUILayout.Button("Engage"))
        {
            _GameManager.LaunchEngage(false);
        }
    }

}

public static class Debugging
{
    public static void Showlog(this object o)
    {
        Debug.Log(o);
    }
}
