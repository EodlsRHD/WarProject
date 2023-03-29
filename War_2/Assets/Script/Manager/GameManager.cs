using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tool.File;
using Tool.Factory;

public enum eCharacterType
{
    Non,
    warrior,
    archer,
    wizard
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [Header("Tool")]
    private SceneChanger _sceneChanger = null;

    [SerializeField]
    private File _file = null;

    [SerializeField]
    private InstantiateFactory _instantiateFactory = null;

    [Space(30)]

    [Header("Size")]
    [SerializeField]
    private int _nodeSize = 1;

    [SerializeField]
    private int _chunkSize = 10;

    [SerializeField]
    private float _topHeight = 100f;


    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    public File file
    {
        get { return _file; }
    }

    public InstantiateFactory instantiateFactory
    {
        get { return _instantiateFactory; }
    }

    public int nodeSize
    {
        get { return _nodeSize; }
    }

    public int chunkSize
    {
        get { return _chunkSize; }
    }

    public float topHeight
    {
        get { return _topHeight; }
    }

    private void Awake()
    {
        _instance = this;

        Application.targetFrameRate = 60;

        DontDestroyOnLoad(this.gameObject);
    }

    public void SelectMapCharacter(string mapName, eCharacterType characterType)
    {
        SceneChanger.LoadIngameScene("InGame", eSceneType.InGame, characterType, mapName);
    }
}
