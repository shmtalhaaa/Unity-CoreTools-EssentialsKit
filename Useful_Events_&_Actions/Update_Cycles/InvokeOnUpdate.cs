using UnityEngine;

[DisallowMultipleComponent]
public class InvokeOnUpdate : BaseUpdateInvokeEvent
{
    private void Update()
    {
        TryInvoke();
    }
}
