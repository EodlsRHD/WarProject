using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProduction : MonoBehaviour
{
    [SerializeField]
    private Building _building = null;

    [SerializeField]
    private Transform _spawnPoint = null;

    [SerializeField]
    private Unit _productionUnit = null;

    [SerializeField]
    private float _productionTime = 0f;

    private Transform _rallyPoint = null;

    private Stack<Unit> _productionUnitPool = new Stack<Unit>();

    private float _time = 0f;

    private int _liveUnitCount = 0;

    private bool _isGameStart = false;

    private bool _isProduction = false;

    public int liveUnitCount
    {
        get { return _liveUnitCount; }
    }

    public bool isProduction
    {
        set { _isProduction = value; }
    }

    public void Initialize(bool isGamestart, eObjColor objColor)
    {
        _isGameStart = isGamestart;

        _rallyPoint = CorpsManager.Instance.GetRallyPoint(objColor);

        for (int i = 0; i < 5; i++)
        {
            Unit obj = GameManager.Instance.instantiateFactory.ObjectInstantiate<Unit>(_productionUnit.gameObject, this.gameObject.transform, _spawnPoint.position);
            obj.Prodiction(ReturnPool, _rallyPoint, _building.objColor.objColor);

            _productionUnitPool.Push(obj);
        }
    }

    private void Update()
    {
        if(_isGameStart == false)
        {
            return;
        }

        if(_isProduction == false)
        {
            return;
        }

        _time += Time.deltaTime;

        if(_time >= _productionTime)
        {
            InstantiateUnit();

            _time = 0f;
        }
    }

    private void InstantiateUnit()
    {
        if(_productionUnitPool.Count <= 0)
        {
            Unit obj = GameManager.Instance.instantiateFactory.ObjectInstantiate<Unit>(_productionUnit.gameObject, this.gameObject.transform, _spawnPoint.position);
            obj.Prodiction(ReturnPool, _rallyPoint, _building.objColor.objColor);

            _productionUnitPool.Push(obj);
        }

        Unit unit = _productionUnitPool.Pop();
        unit.Active();

        _liveUnitCount++;
    }

    private void ReturnPool(Unit unit)
    {
        if(_productionUnitPool.Count >= 5)
        {
            Destroy(unit.gameObject);
            return;
        }

        _productionUnitPool.Push(unit);

        _liveUnitCount--;
    }
}
