using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{
    private static IngameUIManager _instance;

    public static IngameUIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new IngameUIManager();
            }

            return _instance;
        }
    }

    [SerializeField]
    private Image _loadingImage = null;

    private void Awake()
    {
        _instance = this;
    }

    public void SetLoadingImageActive(bool boolean)
    {
        _loadingImage.gameObject.SetActive(boolean);
    }
}
