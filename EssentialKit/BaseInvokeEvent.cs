using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInvokeEvent : MonoBehaviour
{
    #region Variables

    [Tooltip("Delay in function call")]
    [SerializeField, Min(0f)] private float delay = 0f;
    public float DelayInCall
    {
        get => delay;
        set => delay = Mathf.Max(0, value);
    }
    protected bool hasExecuted = false;
    [SerializeField, Space(10)] private UnityEvent invokeEvent;
    public event Action invokeAction;

    [Header("Disable After Execution")]
    [SerializeField] private bool disableGameObject = false;
    [SerializeField] private bool disableComponent = false;

    private Coroutine invokedCoroutine;

    #endregion

    // Public API to invoke the UnityEvent and Action after a custom delay at runtime.
    public void RecallFunction(float delayInSeconds = 0)
    {
        DelayInCall = delayInSeconds;
        if (invokedCoroutine != null)
            StopInvokedCoroutine();
        invokedCoroutine = StartCoroutine(InvokeAfterDelay());
    }

    // Coroutine that waits for the delay, then invokes assigned UnityEvent and Action.
    private IEnumerator InvokeAfterDelay()
    {
        if (DelayInCall > 0f)
            yield return new WaitForSeconds(DelayInCall);

        if (gameObject.activeInHierarchy)
        {
            invokeEvent?.Invoke();
            invokeAction?.Invoke();
            hasExecuted = true;
        }

        invokedCoroutine = null;

        enabled = !disableComponent;
        gameObject.SetActive(!disableGameObject);
    }

    // Stops the currently running coroutine safely.
    private void StopInvokedCoroutine()
    {
        if (invokedCoroutine != null)
        {
            StopCoroutine(invokedCoroutine);
            invokedCoroutine = null;
        }
    }

    // Called when the component is disabled to ensure no coroutine keeps running.
    private void OnDisable() => StopInvokedCoroutine();

    // Called when the object is destroyed to prevents coroutine leaks.
    private void OnDestroy() => StopInvokedCoroutine();
}
