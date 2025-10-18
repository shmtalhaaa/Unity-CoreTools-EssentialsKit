using UnityEngine;

public class InvokeWithDelay : DelayedBaseInvokeEvent
{
    [Header("Re-Execute When Enabled Again")]
    [SerializeField] private bool onEnable = false;

    private void OnEnable()
    {
        if (hasExecuted && onEnable)
            RecallFunction(DelayInCall);
    }
}
