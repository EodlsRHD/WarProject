using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Manager;

public enum eObjColor
{
    Non,
    red,
    blue
}

public enum eObjType
{
    Non,
    building,
    unit
}

public class ObjColor : MonoBehaviour
{
    [SerializeField]
    private eObjType _objType = eObjType.Non;

    [SerializeField]
    private eObjColor _objColor = eObjColor.Non;

    [SerializeField]
    private MeshRenderer _meshRenderer = null;

    public eObjColor objColor
    {
        get { return _objColor; }
        set { _objColor = value; }
    }

    public void InitializeBuilding()
    {
        _meshRenderer.material = ColorManager.Instance.Change_BuildingColor(_objColor);
    }

    public void InitializeUnit()
    {
        _meshRenderer.material = ColorManager.Instance.Change_UnitColor(_objColor);
    }
}
