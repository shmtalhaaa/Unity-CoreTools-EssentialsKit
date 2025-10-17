using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class SimpleBaseInvokeEvent : MonoBehaviour
{
    [SerializeField, Space(10)] protected UnityEvent invokeEvent;
    public event Action invokeAction;

    // Public API to invoke the UnityEvent and Action after a custom updateDelay at runtime.
    public void RecallFunction()
    {
        LogicToBeExecuted();
    }

    // Main logic execution
    protected virtual void LogicToBeExecuted()
    {
        invokeEvent?.Invoke();
        invokeAction?.Invoke();
    }

}
