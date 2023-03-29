using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateTransitionBase
{
    private StateData _destinationState = null;

    private Action<CharacterInfo> _onCallbackOnExitState = null;

    private Action<Action<CharacterInfo, Action>> _onCallbackSetUpdataMethod = null;

    public StateData destinationState
    {
        get { return _destinationState; }
    }

    public StateTransitionBase(StateData destinationState, Action<Action<CharacterInfo, Action>> onCallbackSetUpdataMethod)
    {
        _destinationState = destinationState;
        _onCallbackOnExitState = destinationState._state.OnExitState;
        _onCallbackSetUpdataMethod = onCallbackSetUpdataMethod;
    }

    public bool StateTranstionConditionAndOnEnterState(ref CharacterInfo character, ref List<CharacterInfo> objectDetected)
    {
        if (objectDetected.Count > 0)
        {
            character.NearestObject(ref objectDetected);
        }

        if (_destinationState._state.Condition(ref character, ref objectDetected))
        {
            _onCallbackOnExitState(character);
            _destinationState._state.OnEnterState(ref character, ref objectDetected, _onCallbackSetUpdataMethod);
            return true;
        }

        return false;
    }
}

public class CharacterStateManager : MonoBehaviour
{
    [SerializeField]
    private CharacterInfo _character = null;

    [SerializeField]
    private StateContainer _container = null;

    private bool _useAroundSearch = true;

    [SerializeField]
    private StateData _state = null;

    [SerializeField]
    private Dictionary<int, List<StateTransitionBase>> _stateTransitions = new Dictionary<int, List<StateTransitionBase>>();

    private Action<CharacterInfo, Action> _onCallbackOnStayState = null;

    private void Start() // TestCode
    {
        _character.Initialize();
        Initialize(_character);
    }

    public void Initialize(CharacterInfo character)
    {
        _character = character;
        _stateTransitions = _container.SetTransition(SetUpdateMethod);

        _character.AroundSearch(_useAroundSearch, _container.searchTag, (objectDetected) =>
        {
            foreach (var state in _container.states)
            {
                if (_container.idleStateID != state._id)
                {
                    continue;
                }

                _state = state;
                state._state.OnEnterState(ref character, ref objectDetected, SetUpdateMethod);
                break;
            }
        });
    }

    private void SetUpdateMethod(Action<CharacterInfo, Action> onCallbackOnStayState)
    {
        _onCallbackOnStayState = onCallbackOnStayState;
    }

    public void ConditionComparison()
    {
        _character.AroundSearch(_useAroundSearch, _container.searchTag, (objectDetected) =>
        {
            foreach(var stateTransition in _stateTransitions[_state._id])
            {
                if (stateTransition.StateTranstionConditionAndOnEnterState(ref _character, ref objectDetected))
                {
                    _state = stateTransition.destinationState;
                    break;
                }
            }
        });
    }

    private void Update()
    {
        if(_onCallbackOnStayState == null)
        {
            return;
        }

        _onCallbackOnStayState(_character, ConditionComparison);

        if(_character.isAction == false)
        {
            ConditionComparison();
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(_character.transform.position, _character.searchRadius);

        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(_character.transform.position, _character.walkRadius);

        if(_character.targetCharacter == null && _character.targetPosition != Vector3.zero)
        {
            if(this.gameObject.CompareTag("Monster"))
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(_character.transform.position, _character.targetPosition);
                Gizmos.DrawWireSphere(_character.targetPosition, 1f);
            }
            else
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(_character.transform.position, _character.targetPosition);
                Gizmos.DrawWireSphere(_character.targetPosition, 1f);
            }
        }

        if(_character.targetCharacter != null)
        {
            if (this.gameObject.CompareTag("Monster"))
            {
                Gizmos.color = Color.black;
                Gizmos.DrawLine(_character.transform.position, _character.targetCharacter.transform.position);
                Gizmos.DrawWireSphere(_character.targetPosition, 2f);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_character.transform.position, _character.targetCharacter.transform.position);
                Gizmos.DrawWireSphere(_character.targetPosition, 2f);
            }
        }
    }
}