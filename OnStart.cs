using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class OnStart : MonoBehaviour
{
    [Tooltip("Delay in function call")]
    [SerializeField, Min(0f)] private float delay = 0f;
    public float DelayInCall
    {
        get => delay;
        set => delay = Mathf.Max(0, value);
    }
    [SerializeField] private bool recallOnEnable = false;

    // Keep this 'true' because the OnEnable calls before Start Function else it will run in Start + Enable function both times
    private bool hasInvoked = true;

    [SerializeField, Space(10)] private UnityEvent invokeOnStart;
    public event Action OnStartActions;

    [Header("After Execution")]
    [SerializeField] private bool disableGameObject = false;
    [SerializeField] private bool disableComponent = false;

    private Coroutine invokedCoroutine;

    private void Start()
    {
        ExecuteStart();
    }

    private void OnEnable()
    {
        if (recallOnEnable && !hasInvoked)
            RecallStartFunction(DelayInCall);
    }

    // API for runtime usage
    public void RecallStartFunction(float delayInSeconds = 0)
    {
        DelayInCall = delayInSeconds;
        StopInvokedCoroutine();
        invokedCoroutine = StartCoroutine(InvokeAfterDelay());
    }

    private void ExecuteStart()
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
            invokeOnStart?.Invoke();
            OnStartActions?.Invoke();
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
