using UnityEngine;

public class InvokeOnTriggerEnter : DelayedBaseInvokeEvent
{
    [Header("Detection Settings")]
    [SerializeField] private string[] tagNames;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tagNames.Length; i++)
            if (other.CompareTag(tagNames[i]))
                RecallFunction();
    }
}