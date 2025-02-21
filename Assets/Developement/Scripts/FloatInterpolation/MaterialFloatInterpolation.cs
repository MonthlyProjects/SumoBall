using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MaterialFloatInterpolation : MonoBehaviour
{
    #region Fields

    private MaterialPropertyBlock[] _propBlocks;
    private Renderer _meshRenderer;
    [SerializeField] private string _shaderPropertyName;
    private int _amount;
    private float _oldAmountBeforeCoroutine;
    [SerializeField] private float _currentAmount = 0;
    [SerializeField] private float timeToAnimate;
    public Action OnFinishInterpoling;
    [SerializeField] private float loopDuration1;
    [SerializeField] private float loopDuration2;
    [SerializeField] private bool useCurve;
    [SerializeField] private AnimationCurve interpolationCurve;



    #endregion

    private void Awake()
    {
        _amount = Shader.PropertyToID(_shaderPropertyName);
        _meshRenderer = GetComponent<Renderer>();
        Init(_meshRenderer);
    }

    #region Logic
    public void Init(Renderer meshRenderer)
    {
        _meshRenderer = meshRenderer;
        _propBlocks = new MaterialPropertyBlock[meshRenderer.sharedMaterials.Length];
        for (int i = 0; i < _propBlocks.Length; i++)
        {
            _propBlocks[i] = new MaterialPropertyBlock();
        }
    }

    public void SetValue(int propertyID, float amount)
    {
        for (int i = 0; i < _propBlocks.Length; i++)
        {
            // Get the current value of the material properties in the renderer.
            _meshRenderer.GetPropertyBlock(_propBlocks[i], i);
            // Assign our new value.
            var value = Mathf.Clamp(amount, 0, 1);
            if(useCurve)
            {
                _propBlocks[i].SetFloat(_amount, interpolationCurve.Evaluate(value));
            }
            else
            {
                _propBlocks[i].SetFloat(_amount, value);
            }

            _currentAmount = value;
            // Apply the edited values to the renderer.
            _meshRenderer.SetPropertyBlock(_propBlocks[i], i);
        }
    }
    [EasyButtons.Button]
    public void InterpolateValueByDuration(float duration, float amount)
    {
        StopAllCoroutines();
        SetValue(_amount, _oldAmountBeforeCoroutine);
        StartCoroutine(InterpolateValue(duration, amount));
    }
    public void InterpolateValueByDuration(float amount)
    {
        StopAllCoroutines();
        if(timeToAnimate == 0)
        {
            SetValue(_amount, amount);
            return;
        }
        StartCoroutine(InterpolateValue(timeToAnimate, amount));
    }

    IEnumerator InterpolateValue(float duration, float amount)
    {
        float time = 0;
        float oldAmount = _currentAmount;
        _oldAmountBeforeCoroutine = oldAmount;

        while (time <= duration)
        {
            time += Time.deltaTime;
            float lerpValue = time / duration;
            SetValue(_amount, Mathf.Lerp(oldAmount,amount,lerpValue));
            yield return null;
        }
        SetValue(_amount, amount);
        OnFinishInterpoling?.Invoke();
    }

    public void AnimateInLoop()
    {
        AnimateValueInLoop(loopDuration1, loopDuration2, 1);
    }

    [EasyButtons.Button]
    public void AnimateValueInLoop(float duration1, float duration2, float amount)
    {
        //if(!gameObject.activeSelf) { return; }
        StopAllCoroutines();
        SetValue(_amount, _oldAmountBeforeCoroutine);
        StartCoroutine(AnimateValueInLoopCoroutine(duration1, duration2, amount));
    }

    IEnumerator AnimateValueInLoopCoroutine(float duration1, float duration2, float amount)
    {
        float time = 0;
        float oldAmount = _currentAmount;
        _oldAmountBeforeCoroutine = oldAmount;
        while (time <= duration1)
        {
            time += Time.deltaTime;
            float lerpValue = time / duration1;
            SetValue(_amount, Mathf.Lerp(oldAmount, amount, lerpValue));
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time <= duration2)
        {
            time += Time.deltaTime;
            float lerpValue = time / duration2;
            SetValue(_amount, Mathf.Lerp(amount, oldAmount, lerpValue)); 
            yield return new WaitForEndOfFrame();
        }

        SetValue(_amount, oldAmount);
        OnFinishInterpoling?.Invoke();
    }

    #endregion
}
