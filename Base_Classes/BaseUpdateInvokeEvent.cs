using UnityEngine;

public abstract class BaseUpdateInvokeEvent : SimpleBaseInvokeEvent
{
    [Header("Delay Settings")]
    [SerializeField, Min(0f)] private float firstDelay = 0f;
    public float FirstDelay
    {
        get => firstDelay;
        set => firstDelay = Mathf.Max(0f, value);
    }

    [SerializeField, Min(0f)] private float updateDelay = 0f;
    public float UpdateDelay
    {
        get => updateDelay;
        set => updateDelay = Mathf.Max(0f, value);
    }

    private float nextInvokeTime;

    protected virtual void Start()
    {
        nextInvokeTime = Time.time + FirstDelay;
    }

    protected virtual void TryInvoke()
    {
        if (Time.time < nextInvokeTime)
            return;

        RecallFunction();
        nextInvokeTime = Time.time + (UpdateDelay == 0f ? Time.deltaTime : UpdateDelay);
    }

    public void ResetFirstDelay(float delay = 0f)
    {
        FirstDelay = delay;
        nextInvokeTime = Time.time + FirstDelay;
    }
}
