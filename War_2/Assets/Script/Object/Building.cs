using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : GameObj.GameObject
{
    [SerializeField]
    private BuildingProduction _buildingProduction = null;

    public void Initialize()
    {
        InitializeBuildingColor();

        _buildingProduction.Initialize(true, _objColor.objColor);
    }

    public void StartProuctionUnit()
    {
        _buildingProduction.isProduction = true;
    }
}
