using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private int _sceneIndex = 1;
    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
  
}
