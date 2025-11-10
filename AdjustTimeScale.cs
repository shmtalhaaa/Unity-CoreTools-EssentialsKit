using UnityEngine;

public class AdjustTimeScale : MonoBehaviour
{
    private float defaultTimeScale;
    private float defaultFixedDeltaTime;

    [SerializeField, Range(0f, 1f)] private float targetScale = 1f;
    public float TargetScale
    {
        get => targetScale;
        set => targetScale = Mathf.Clamp01(value);
    }

    [SerializeField] private bool smoothTransition = false;
    [SerializeField, Min(0f)] private float transitionSpeed = 3f;

    [Header("Physics safe")]
    [SerializeField] private bool scalePhysics = false;

    private void Awake()
    {
        defaultTimeScale = Time.timeScale;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void SetTargetScale(float newTargetScale, float newTransitionSpeed = 0f)
    {
        TargetScale = Mathf.Clamp01(newTargetScale);
        transitionSpeed = Mathf.Max(0f, newTransitionSpeed);
    }

    private void ApplyTimeScaleInstantly(float timeScale)
    {
        Time.timeScale = timeScale;
        if (scalePhysics) Time.fixedDeltaTime = defaultFixedDeltaTime * timeScale;
    }

    private void ResetTimeScale()
    {
        Time.timeScale = defaultTimeScale;
        if (scalePhysics) Time.fixedDeltaTime = defaultFixedDeltaTime;
    }

    private void OnDisable() => ResetTimeScale();

    private void OnDestroy() => ResetTimeScale();

#if UNITY_EDITOR
    private void OnApplicationQuit() => ResetTimeScale();
#endif

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, TargetScale)) return;
        float timeScale = smoothTransition && transitionSpeed > 0f ? Mathf.Lerp(Time.timeScale, TargetScale, Time.unscaledDeltaTime * transitionSpeed) : TargetScale;
        ApplyTimeScaleInstantly(timeScale);
    }
}
