using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Manager;

public enum eSceneType
{
    Non,
    Title,
    Enter,
    InGame
}

public class SceneChanger : MonoBehaviour
{
    private static string _nextScene;

    [SerializeField]
    private MapManager _mapManager = null;

    [SerializeField]
    private Image _fildBar;

    private static string _mapName = string.Empty;

    private float _mapMakeIsDone = 0f;

    private static eSceneType _sceneType = eSceneType.Non;

    private static eCharacterType _characterType = eCharacterType.Non;

    public float mapMakeIsDone
    {
        get { return _mapMakeIsDone; }
        set{ _mapMakeIsDone = value; }
    }

    public static void LoadIngameScene(string _scenename, eSceneType sceneType, eCharacterType characterType, string mapName = null)
    {
        _mapName = mapName;
        _nextScene = _scenename;
        _sceneType = sceneType;
        _characterType = characterType;

        SceneManager.LoadScene("Loading");
    }

    void Start()
    {
        switch(_sceneType)
        {
            case eSceneType.Enter:

                return;

            case eSceneType.InGame:

                if (_mapName == null)
                {
                    return;
                }

                _mapManager.Initialize(_mapName, _characterType);

                StartCoroutine(LoadSceneProcess());

                _mapManager.SelectMap();

                return;
        }
    }

    IEnumerator LoadSceneProcess()
    {
        _mapMakeIsDone = 0f;

        AsyncOperation ao = SceneManager.LoadSceneAsync(_nextScene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            yield return null;

            if (_mapMakeIsDone < 0.9f)
            {
                _fildBar.fillAmount = _mapMakeIsDone;
            }
            else
            {
                _mapMakeIsDone += Time.unscaledDeltaTime;
                _fildBar.fillAmount = Mathf.Lerp(0.9f, 1f, _mapMakeIsDone);
                if (_fildBar.fillAmount >= 1f)
                {
                    ao.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
