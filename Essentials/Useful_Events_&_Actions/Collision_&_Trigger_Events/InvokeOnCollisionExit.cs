using UnityEngine;

public class InvokeOnCollisionExit : DelayedBaseInvokeEvent
{
    [Header("Detection Settings")]
    [SerializeField] private string[] tagNames;

    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < tagNames.Length; i++)
            if (collision.gameObject.CompareTag(tagNames[i]))
                RecallFunction();
    }
}
