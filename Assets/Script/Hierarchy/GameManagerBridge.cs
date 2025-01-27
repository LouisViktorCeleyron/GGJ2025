using TMPro;
using UnityEngine;

public class GameManagerBridge : VolleyBulleGO
{

    [SerializeField]
    private Transform _engageTransformL, _engageTransformR;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private bool _mainMenu;
    void Start()
    {
        if(_mainMenu)
        {
            _GameManager.ResetGM();
        }
        else
        {
            _GameManager.engageTransformL = _engageTransformL;
            _GameManager.engageTransformR = _engageTransformR;
            _GameManager.timerText = _timerText;
            _GameManager.StartGameplay();
        }
    }
}
