using System.Collections;
using UnityEngine;

public abstract class DelayedBaseInvokeEvent : SimpleBaseInvokeEvent
{
    #region Variables

    [Header("Delay in seconds")]
    [SerializeField, Min(0f)] private float delay = 0f;
    public float DelayInCall
    {
        get => delay;
        set => delay = Mathf.Max(0, value);
    }
    protected bool hasExecuted = false;

    [Header("Disable After Execution")]
    [SerializeField] protected bool disableGameObject = false;
    [SerializeField] protected bool disableComponent = false;

    private Coroutine invokedCoroutine;

    #endregion

    // Public API to invoke the UnityEvent and Action after a custom updateDelay at runtime.
    public void RecallFunction(float delayInSeconds = 0)
    {
        DelayInCall = delayInSeconds;
        if (invokedCoroutine != null)
            StopInvokedCoroutine();

        if (DelayInCall > 0f)
            invokedCoroutine = StartCoroutine(InvokeAfterDelay());
        else
            LogicToBeExecuted();
    }

    // Coroutine that waits for the updateDelay, then invokes assigned UnityEvent and Action.
    private IEnumerator InvokeAfterDelay()
    {
        yield return new WaitForSeconds(DelayInCall);
        LogicToBeExecuted();
    }

    // Main logic execution
    protected override void LogicToBeExecuted()
    {
        if (gameObject.activeInHierarchy)
        {
            base.LogicToBeExecuted();
            hasExecuted = true;
        }

        invokedCoroutine = null;

        if (disableComponent)
            enabled = false;
        if (disableGameObject)
            gameObject?.SetActive(false);
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
    protected virtual void OnDisable() => StopInvokedCoroutine();

    // Called when the object is destroyed to prevents coroutine leaks.
    protected virtual void OnDestroy() => StopInvokedCoroutine();
}
