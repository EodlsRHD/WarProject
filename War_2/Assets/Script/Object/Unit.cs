using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Unit : GameObj.GameObject
{
    [SerializeField]
    private NavMeshAgent _agent = null;

    private Action<Unit> _onCallbackReturnPool = null;

    private Transform _rallyPoint = null;

    public void Initialize()
    {
        
    }

    public void Prodiction(Action<Unit> onCallbackRestrnPool, Transform rallyPoint, eObjColor objColor)
    {
        this.gameObject.SetActive(false);

        _onCallbackReturnPool = onCallbackRestrnPool;
        _rallyPoint = rallyPoint;
        _objColor.objColor = objColor;

        InitializeUnitColor();
    }

    public void Active()
    {
        this.gameObject.SetActive(true);

        _agent.destination = _rallyPoint.position;
    }
}