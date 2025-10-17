using UnityEngine;

[DisallowMultipleComponent]
public class InvokeOnFixedUpdate : BaseUpdateInvokeEvent
{
    private void FixedUpdate()
    {
        TryInvoke();
    }
}
