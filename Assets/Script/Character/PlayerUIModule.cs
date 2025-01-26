using UnityEngine;
using TMPro;

public class PlayerUIModule : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoretext;

    public void UpdateScore(int score)
    {
        _scoretext.text = score.ToString("00");
    }


}
