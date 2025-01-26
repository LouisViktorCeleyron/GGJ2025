using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIModule : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoretext;

    [SerializeField]
    Image _face, _breath;

    public void UpdateScore(int score)
    {
        _scoretext.text = score.ToString("00");
    }


    public void Init(CharacterData data)
    {
        _face.sprite = data.icon; 
    }


    public void UpdareBreath(float amount)
    {
        _breath.fillAmount = amount;
    }
}
