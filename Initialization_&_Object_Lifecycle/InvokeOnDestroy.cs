using UnityEngine;

public class InvokeOnDestroy : SimpleBaseInvokeEvent
{
    protected void OnDestroy()
    {
        RecallFunction();
    }

    [ContextMenu("Destroy!")]
    private void DestroyThis()
    {
        DestroyImmediate(gameObject);
    }

}
