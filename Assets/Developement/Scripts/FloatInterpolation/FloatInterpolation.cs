using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class FloatInterpolation : MonoBehaviour
{
    #region Fields
    private float _currentAmount = 0;
    public float CurrentAmount { get { return _currentAmount; } set { SetValue(value);} }
    [SerializeField] private bool _unscaledTimeScale;

    [SerializeField] protected float loopDurationStart, loopWaitDuration, loopDurationEnd, loopStartvalue, loopTargetValue;

    [SerializeField] private float singleDuration, singleTargetValue;
    protected bool constantLoop;

    public UnityEvent OnLoopStarted;
    public UnityEvent OnLoopIsAtMiddle;
    public UnityEvent OnLoopIsAtMidle2;

    public UnityEvent OnLoopEnded;

    public UnityEvent OnSingleStarted;
    public UnityEvent OnSingleEnded;
    #endregion

    #region Logic

    public abstract void SetValue(float value);
    [EasyButtons.Button]
    public void InterpolateValueByDuration(float duration, float amount)
    {
        StopAllCoroutines();
        StartCoroutine(InterpolateValue(duration, amount));
    }
    [EasyButtons.Button]
    public void InterpolateValueByDuration()
    {
        StopAllCoroutines();
        StartCoroutine(InterpolateValue(singleDuration, singleTargetValue));
    }

    public void InterpolateValueByDuration(float targetValue)
    {
        StopAllCoroutines();
        StartCoroutine(InterpolateValue(singleDuration, targetValue));
    }

    protected IEnumerator InterpolateValue(float duration, float amount)
    {
        OnSingleStarted?.Invoke();
        float time = 0;
        float oldAmount = _currentAmount;

        while (time <= duration)
        {
            time += _unscaledTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            float lerpValue = time / duration;
            SetValue(Mathf.Lerp(oldAmount, amount, lerpValue));
            yield return null;
        }
        _currentAmount = amount;
        SetValue(amount);
        OnSingleEnded?.Invoke();
    }

    [EasyButtons.Button]
    public void AnimateValueInLoop(float duration1, float duration2, float amount)
    {
        StartCoroutine(AnimateValueInLoopCoroutine(duration1, duration2, amount));
    }
    [EasyButtons.Button]
    public void AnimateValueInLoop()
    {
        StartCoroutine(AnimateValueInLoopCoroutine(loopDurationStart, loopDurationEnd, loopTargetValue));
    }

    protected IEnumerator AnimateValueInLoopCoroutine(float duration1, float duration2, float amount)
    {
        OnLoopStarted?.Invoke();
        float time = 0;
        float oldAmount = loopStartvalue;
        while (time <= duration1)
        {
            time += _unscaledTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            float lerpValue = time / duration1;
            SetValue(Mathf.Lerp(oldAmount, amount, lerpValue));
            yield return null;
        }
        OnLoopIsAtMiddle?.Invoke();
        if(_unscaledTimeScale)
        {
            yield return new WaitForSecondsRealtime(loopWaitDuration);
        }
        else
        {
            yield return new WaitForSeconds(loopWaitDuration);
        }
        OnLoopIsAtMidle2?.Invoke();
        time = 0;
        while (time <= duration2)
        {
            time += _unscaledTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            float lerpValue = time / duration2;
            SetValue(Mathf.Lerp(amount, oldAmount, lerpValue));
            yield return null;
        }

        SetValue(oldAmount);
        OnLoopEnded?.Invoke();
        if(constantLoop)
        {
            AnimateValueInLoop();
        }
    }
    #endregion
}
