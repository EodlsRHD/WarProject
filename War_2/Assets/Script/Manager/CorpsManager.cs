using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Corps
{
    public eObjColor objColor = eObjColor.Non;

    public Transform rallyPoint = null;
}

public class CorpsManager : MonoBehaviour
{
    private static CorpsManager _instance;
    public static CorpsManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new CorpsManager();
            }

            return _instance;
        }
    }

    [SerializeField]
    private List<Corps> _rellyPoints = new List<Corps>();

    private void Awake()
    {
        _instance = this;
    }

    public void SetRallyPoint(Corps corps)
    {
        _rellyPoints.Add(corps);
    }

    public Transform GetRallyPoint(eObjColor objColor)
    {
        foreach(var corps in _rellyPoints)
        {
            if(corps.objColor == objColor)
            {
                return corps.rallyPoint;
            }
        }

        return null;
    }
}
