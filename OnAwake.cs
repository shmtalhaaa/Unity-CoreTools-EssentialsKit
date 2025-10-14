using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class OnAwake : MonoBehaviour
{
    [Tooltip("Delay in function call")]
    [SerializeField, Min(0f)] private float delay = 0f;
    public float DelayInCall
    {
        get => delay;
        set => delay = Mathf.Max(0, value);
    }
    [SerializeField] private bool recallOnEnable = false;
    private bool hasInvoked = false;

    [SerializeField, Space(10)] private UnityEvent invokeOnAwake;
    public event Action OnAwakeActions;

    [Header("After Execution")]
    [SerializeField] private bool disableGameObject = false;
    [SerializeField] private bool disableComponent = false;

    private Coroutine invokedCoroutine;

    private void Awake()
    {
        if (enabled == false) return;
        hasInvoked = true;
        ExecuteAwake();
    }

    private void OnEnable()
    {
        if (recallOnEnable && !hasInvoked)
            RecallAwakeFunction(DelayInCall);
    }

    // API for runtime usage
    public void RecallAwakeFunction(float delayInSeconds = 0)
    {
        DelayInCall = delayInSeconds;
        StopInvokedCoroutine();
        invokedCoroutine = StartCoroutine(InvokeAfterDelay());
    }

    private void ExecuteAwake()
    {
        if (invokedCoroutine != null) return;
        invokedCoroutine = StartCoroutine(InvokeAfterDelay());
    }

    private IEnumerator InvokeAfterDelay()
    {
        if (DelayInCall > 0f)
            yield return new WaitForSeconds(DelayInCall);

        if (gameObject.activeInHierarchy)
        {
            invokeOnAwake?.Invoke();
            OnAwakeActions?.Invoke();
        }

        invokedCoroutine = null;

        gameObject.SetActive(!disableGameObject);
        enabled = !disableComponent;
    }

    private void StopInvokedCoroutine()
    {
        hasInvoked = false;
        if (invokedCoroutine != null)
        {
            StopCoroutine(invokedCoroutine);
            invokedCoroutine = null;
        }
    }

    // Coroutines are stopped on Disable and Destroy, these functions prevents leaks or ghost invokes.
    private void OnDisable() => StopInvokedCoroutine();

    private void OnDestroy() => StopInvokedCoroutine();

}
