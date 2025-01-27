using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : VolleyBulleGO
{

    [SerializeField]
    private GameObject _egality;
    [SerializeField]
    private TextMeshProUGUI _victoryText;
    [SerializeField]
    private UISelectionChar _charWin, _charEg1,_charEg2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_GameManager.Winner != null)
        {
            _charWin.UpdateChar(_GameManager.Winner);    
        }
        else
        {
            _charEg1.UpdateChar(_GameManager.LeftChar);
            _charEg2.UpdateChar(_GameManager.RightChar);
            _victoryText.text = "DRAW";
            _charWin.gameObject.SetActive(false);
            _egality.SetActive(true);
        }

    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
