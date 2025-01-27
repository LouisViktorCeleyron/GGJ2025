using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharaPresLoad : MonoBehaviour
{

    [SerializeField]
    private CharacterData _characterData;
    [SerializeField]
    private TextMeshProUGUI _name, _desc;
    [SerializeField]
    private Image _image;


    void Start()
    {
        _name.text = _characterData.characterName;
        _desc.text = _characterData.characterDescription;
        StartCoroutine(Switch());
    }

    private IEnumerator Switch()
    {
        while (true) 
        {
            _image.sprite = _characterData.spriteIdle;
            yield return new WaitForSeconds(5);
            _image.sprite = _characterData.spriteBlow;
            yield return new WaitForSeconds(5);
            _image.sprite = _characterData.icon;
            yield return new WaitForSeconds(5);
            _image.sprite = _characterData.iconH;
            yield return new WaitForSeconds(5);
            _image.sprite = _characterData.InconU;

        }
    }
}
