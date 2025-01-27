using UnityEngine;

public class CharaSelection : VolleyBulleGO
{
    [SerializeField]
    private bool _player2;
    [SerializeField]
    private float[] _xPos;

    [SerializeField]
    private UISelectionChar[] _selectableCharacter;

    private int _indexSelected;

    [SerializeField]
    private RectTransform _rectTransform;

    private string _hAxName;
    private string _buttonName;

    [SerializeField]
    private KeyCode _launch;

    private bool _inputClocked;

    private bool _IsSelected => _player2?_GameManager.LeftSelected:_GameManager.RightSelected;

    private void Awake()
    {
        _buttonName = _player2 ? "Fire2" : "Fire1";
        _hAxName = _player2 ? "Horizontal2" : "Horizontal";
    }

    private void Start()
    {
        SelectChar(0);
    }

    private void Update()
    {
        if (_IsSelected)
        {
            return;
        }
        var axisV = Input.GetAxisRaw(_hAxName);
        if (axisV!=0)
        {
            if(!_inputClocked)
            {
                _inputClocked = true;
                _indexSelected = IntExtention.LoopedClamp( _indexSelected + (int)Mathf.Sign(axisV), 0, _selectableCharacter.Length-1);
                SelectChar(_indexSelected);
            }
        }
        else
        {
            _inputClocked = false;
        }
        if(Input.GetKeyDown(_launch)) 
        {
            _selectableCharacter[_indexSelected].Select(_player2);
        }
    }

    private void SelectChar(int index)
    {
        _rectTransform.anchoredPosition = new Vector2(_xPos[index], _rectTransform.anchoredPosition.y);
    }
}
