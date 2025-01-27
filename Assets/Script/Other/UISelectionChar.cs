using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISelectionChar : VolleyBulleGO
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private Image _face;
    [SerializeField]
    private CharacterData _characterData;
    void Start()
    {
        _name .text= _characterData.characterName;
        _face.sprite = _characterData.icon;
    }

    public void Select(bool playerTwo)
    {
        _face.sprite = _characterData.iconH;
        _GameManager.SetPlayer(_characterData, playerTwo);
    }

}
