using UnityEngine;

public class InvokeOnTriggerExit : DelayedBaseInvokeEvent
{
    [Header("Detection Settings")]
    [SerializeField] private string[] tagNames;

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < tagNames.Length; i++)
            if (other.CompareTag(tagNames[i]))
                RecallFunction(DelayInCall);
    }
}