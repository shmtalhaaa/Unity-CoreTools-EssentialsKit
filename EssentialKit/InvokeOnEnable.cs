using UnityEngine;

[DisallowMultipleComponent]
public class InvokeOnEnable : BaseInvokeEvent
{
    private void OnEnable()
    {
        RecallFunction(DelayInCall);
    }
}
