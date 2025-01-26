using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISelectionChar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private Image _face;
    [SerializeField]
    private CharacterData _characterData;
    void Start()
    {
        _name .text= _characterData.name;
        _face.sprite = _characterData.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
