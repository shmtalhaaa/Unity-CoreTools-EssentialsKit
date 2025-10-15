using UnityEngine;

[DisallowMultipleComponent]
public class InvokeOnStart : BaseInvokeEvent
{
    [Header("Re-Execute When Enabled Again")]
    [SerializeField] private bool onEnable = false;

    private void Start()
    {
        RecallFunction(DelayInCall);
    }

    private void OnEnable()
    {
        if (hasExecuted && onEnable)
            RecallFunction(DelayInCall);
    }
}
