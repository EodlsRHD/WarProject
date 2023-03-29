using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State_ : ScriptableObject
{
    public virtual bool Condition(ref CharacterInfo character, ref List<CharacterInfo> objectDetected)
    {
        return false;
    }

    public virtual void OnEnterState(ref CharacterInfo character, ref List<CharacterInfo> objectDetected, Action<Action<CharacterInfo, Action>> onCallbackSetUpdate)
    {

    }

    public virtual void OnStayState(CharacterInfo character, Action onCallbackConditionComparison)
    {

    }

    public virtual void OnExitState(CharacterInfo character)
    {

    }
}
