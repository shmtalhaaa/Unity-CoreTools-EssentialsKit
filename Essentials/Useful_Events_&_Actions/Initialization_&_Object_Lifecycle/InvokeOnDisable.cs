using UnityEngine;

[DisallowMultipleComponent]
public class InvokeOnDisable : SimpleBaseInvokeEvent
{
    private void OnDisable()
    {
        RecallFunction();
    }
}
