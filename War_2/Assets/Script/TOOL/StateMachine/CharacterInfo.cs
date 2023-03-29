using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.AI.NavMeshAgent _navMeshAgent = null;

    [Header("Stat")]
    [SerializeField]
    private float _speed = 0f;

    [SerializeField]
    private float _damage = 10f;

    [SerializeField]
    private float _healthPoint = 30f;

    [Header("Info")]
    [SerializeField]
    private float _interactionDistance = 0f;

    [SerializeField]
    private float _searchRadius = 30f;

    [SerializeField]
    private float _walkRadius = 30f;

    [SerializeField]
    private float _attackRate = 3f;

    [SerializeField]
    private float _distanceFromTarget = 0f;

    [SerializeField]
    private bool _isUnderAttack = false;

    private int _layerMask = 0;

    private float _time = 0;

    private bool _isAction = false;

    private Vector3 _oneSecBeforePosition = Vector3.zero;
    
    [Header("Target Info")]
    [SerializeField]
    private Vector3 _targetPosition = Vector3.zero;

    [SerializeField]
    private CharacterInfo _targetCharacter = null;

    [SerializeField]
    private List<CharacterInfo> _objectDetected = new List<CharacterInfo>();

    private Collider[] colls;

    public UnityEngine.AI.NavMeshAgent navMeshAgent
    {
        get { return _navMeshAgent; }
    }

    public float speed
    {
        get { return _speed; }
    }

    public float damage
    {
        get { return _damage; }
    }

    public float healthPoint
    {
        get { return _healthPoint; }
    }

    public float interactionDistance
    {
        get { return _interactionDistance; }
    }

    public float searchRadius
    {
        get { return _searchRadius; }
    }

    public float walkRadius
    {
        get { return _walkRadius; }
    }

    public float attackRate
    {
        get { return _attackRate; }
    }

    public float distanceFromTarget
    {
        get { return _distanceFromTarget; }
    }

    public bool isUnderAttack
    {
        get { return _isUnderAttack; }
        set { _isUnderAttack = value; }
    }

    public float time
    {
        get { return _time; }
        set { _time = value; }
    }

    public bool isAction
    {
        get { return _isAction; }
        set { _isAction = value; }
    }

    public Vector3 targetPosition
    {
        get { return _targetPosition; }
        set { _targetPosition = value; }
    }

    public Vector3 oneSecBeforePosition
    {
        get { return _oneSecBeforePosition; }
        set { _oneSecBeforePosition = value; }
    }

    public CharacterInfo targetCharacter
    {
        get { return _targetCharacter; }
    }

    public void Initialize()
    {
        _distanceFromTarget = _searchRadius;
        _navMeshAgent.speed = _speed;
        _layerMask = 1 << LayerMask.NameToLayer("Unit");
        colls = new Collider[10];
    }

    public void AroundSearch(bool use, string searchTag, System.Action<List<CharacterInfo>> onCallbackObjectDetected)
    {
        if (use == false)
        {
            return;
        }

        if(searchTag == string.Empty || searchTag.Contains("Untagged"))
        {
            return;
        }

        _objectDetected.Clear();

        int collsNum = Physics.OverlapSphereNonAlloc(this.transform.position, _searchRadius, colls, _layerMask);

        for(int i = 0; i < collsNum; i++)
        {
            if(colls[i].gameObject.CompareTag(searchTag))
            {
                colls[i].gameObject.TryGetComponent(out CharacterInfo character);
                _objectDetected.Add(character);
            }
        }

        onCallbackObjectDetected(_objectDetected);
    }

    public void NearestObject(ref List<CharacterInfo> objectDetected)
    {
        CharacterInfo targetCharacter = objectDetected[0];
        float dis = _searchRadius;

        for (int i = 0; i < objectDetected.Count; i++)
        {
            if (dis > Vector3.Distance(objectDetected[i].transform.position, this.transform.position))
            {
                dis = Vector3.Distance(objectDetected[i].transform.position, this.transform.position);
                targetCharacter = objectDetected[i];
            }
        }

        _targetCharacter = targetCharacter;
        _distanceFromTarget = Mathf.Round(Vector3.Distance(_targetCharacter.transform.position, this.transform.position) * 100f) * 0.01f;
    }

    public void Hit(CharacterInfo Attacker, float damage) // 'Character' variable is TestCode
    {
        if(_healthPoint <= 0)
        {
            return;
        }

        _isUnderAttack = true;
        _healthPoint -= damage;
    }

    public void TargetReset()
    {
        _targetCharacter = null;
        _distanceFromTarget = _searchRadius;
    }
}
