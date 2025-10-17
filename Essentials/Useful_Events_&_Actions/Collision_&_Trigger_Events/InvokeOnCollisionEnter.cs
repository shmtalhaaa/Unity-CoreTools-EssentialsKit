using UnityEngine;

public class InvokeOnCollisionEnter : DelayedBaseInvokeEvent
{
    [Header("Detection Settings")]
    [SerializeField] private string[] tagNames;

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < tagNames.Length; i++)
            if (collision.gameObject.CompareTag(tagNames[i]))
                RecallFunction();
    }
}
