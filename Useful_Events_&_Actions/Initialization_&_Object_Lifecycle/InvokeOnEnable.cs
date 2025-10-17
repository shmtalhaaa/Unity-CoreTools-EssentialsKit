using UnityEngine;

[DisallowMultipleComponent]
public class InvokeOnEnable : DelayedBaseInvokeEvent
{
    private void OnEnable()
    {
        RecallFunction(DelayInCall);
    }
}
