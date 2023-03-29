using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnterUIManager : MonoBehaviour
{
    [SerializeField]
    private Button _map1Button = null;

    [SerializeField]
    private Button _map2Button = null;

    [SerializeField]
    private Button _map3Button = null;

    [SerializeField]
    private Button _Character1Buttom = null;

    [SerializeField]
    private Button _Character2Buttom = null;

    [SerializeField]
    private Button _Character3Buttom = null;

    [SerializeField]
    private Button _gameStartButton = null;

    [SerializeField]
    private TMP_Text _selectMap = null;

    [SerializeField]
    private TMP_Text _selectCharacter = null;

    private string _selectMapName = string.Empty;

    private eCharacterType _characterType = eCharacterType.Non;

    private void Start()
    {
        _map1Button.onClick.AddListener(() => { _selectMapName = "TestMap_1"; _selectMap.text = _selectMapName; });
        _map2Button.onClick.AddListener(() => { _selectMapName = "TestMap_2"; _selectMap.text = _selectMapName; });
        _map3Button.onClick.AddListener(() => { _selectMapName = "TestMap_3"; _selectMap.text = _selectMapName; });

        _Character1Buttom.onClick.AddListener(() => { _characterType = eCharacterType.warrior; _selectCharacter.text = _characterType.ToString(); });
        _Character2Buttom.onClick.AddListener(() => { _characterType = eCharacterType.archer; _selectCharacter.text = _characterType.ToString(); });
        _Character3Buttom.onClick.AddListener(() => { _characterType = eCharacterType.wizard; _selectCharacter.text = _characterType.ToString(); });

        _gameStartButton.onClick.AddListener(SelectMapCharacter);
    }

    private void SelectMapCharacter()
    {
        Debug.Log("SelectMap   : " + _selectMapName + "   SelectCharacter   : " + _characterType.ToString());
        GameManager.Instance.SelectMapCharacter(_selectMapName, _characterType);
    }
}
