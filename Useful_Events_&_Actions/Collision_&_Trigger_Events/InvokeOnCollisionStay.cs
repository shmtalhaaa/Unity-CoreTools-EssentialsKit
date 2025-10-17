using UnityEngine;

public class InvokeOnCollisionStay : SimpleBaseInvokeEvent
{
    [Header("Detection Settings")]
    [SerializeField] private string[] tagNames;
    [SerializeField, Min(0f)] private float coolDownTime = 0.1f;
    public float CoolDownTime
    {
        get => coolDownTime;
        set => coolDownTime = Mathf.Max(0f, value);
    }
    private float lastImpactTime = 0f;

    private void OnCollisionStay(Collision collision)
    {
        if (CoolDownTime <= 0f || Time.time - lastImpactTime > CoolDownTime)
        {
            for (int i = 0; i < tagNames.Length; i++)
                if (collision.gameObject.CompareTag(tagNames[i]))
                {
                    lastImpactTime = Time.time;
                    RecallFunction();
                    break;
                }
        }

    }
}
