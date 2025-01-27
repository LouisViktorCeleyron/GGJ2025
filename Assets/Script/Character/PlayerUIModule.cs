using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIModule : VolleyBulleGO
{
    [SerializeField]
    private TextMeshProUGUI _scoretext;

    [SerializeField]
    private Image _face, _otherFace, _breath;
    
    private CharacterData _characterData;
    [SerializeField]
    private bool _player2;

    public void UpdateScore(int score)
    {
        _scoretext.text = score.ToString("00");
        _face.sprite = _characterData.iconH;
        if(_player2)
        {
            _otherFace.sprite = _GameManager.RightChar.InconU;
        }
        else
        {
            _otherFace.sprite = _GameManager.LeftChar.InconU;
        }
        StartCoroutine(ResetIcon());

    }

    private IEnumerator ResetIcon()
    {
        yield return new WaitForSeconds(1);
        _face.sprite = _characterData.icon;
        if (_player2)
        {
            _otherFace.sprite = _GameManager.RightChar.icon;
        }
        else
        {
            _otherFace.sprite = _GameManager.LeftChar.icon;
        }
    }

    public void Init(CharacterData data)
    {
        _characterData = data;
        _face.sprite = data.icon; 
    }


    public void UpdareBreath(float amount)
    {
        _breath.fillAmount = amount;
    }
}
