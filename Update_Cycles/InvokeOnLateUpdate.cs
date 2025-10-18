using UnityEngine;

[DisallowMultipleComponent]
public class InvokeOnLateUpdate : BaseUpdateInvokeEvent
{
    private void LateUpdate()
    {
        TryInvoke();
    }
}
