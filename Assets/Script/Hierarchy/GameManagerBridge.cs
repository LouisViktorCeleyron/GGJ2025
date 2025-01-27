using TMPro;
using UnityEngine;

public class GameManagerBridge : VolleyBulleGO
{

    [SerializeField]
    private Transform _engageTransformL, _engageTransformR;
    [SerializeField] private TextMeshProUGUI _timerText;

    void Start()
    {
        _GameManager.engageTransformL = _engageTransformL;
        _GameManager.engageTransformR = _engageTransformR;
        _GameManager.timerText = _timerText;
        _GameManager.StartGameplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
