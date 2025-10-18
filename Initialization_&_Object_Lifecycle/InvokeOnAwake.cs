using UnityEngine;

[DisallowMultipleComponent]
public class InvokeOnAwake : DelayedBaseInvokeEvent
{
    [Header("Re-Execute When Enabled Again")]
    [SerializeField] private bool onEnable = false;

    private void Awake()
    {
        if (enabled)
            RecallFunction(DelayInCall);
    }

    private void OnEnable()
    {
        if (hasExecuted && onEnable)
            RecallFunction(DelayInCall);
    }
}
