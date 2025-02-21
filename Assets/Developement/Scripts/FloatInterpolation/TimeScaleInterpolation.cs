using System;
using UnityEngine;

public class TimeScaleInterpolation : FloatInterpolation, ITimeScaleable
{
    #region Fields
    private float _timeScale = 1f;
    [SerializeField] private float priority = 10f;
    
    Action<float> _onChangeTimeScale;
    #endregion

    #region Properties
    public float Priority { get { return priority; } }
    public float TimeScale { get { return _timeScale; } set { _timeScale = value; } }
    public Action<float> OnChangeTimeScale { get { return _onChangeTimeScale; } set { _onChangeTimeScale = value; } }

    public GameObject OwnerObject => this.gameObject;

    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        OnLoopStarted.AddListener(AddTimeScale);
        OnLoopEnded.AddListener(RemoveTimeScale);
        OnSingleStarted.AddListener(AddTimeScale);
        //OnSingleEnded.AddListener(RemoveTimeScale);
    }
    private void OnDisable()
    {
        RemoveTimeScale();
        OnLoopStarted.RemoveListener(AddTimeScale);
        OnLoopEnded.RemoveListener(RemoveTimeScale);
        OnSingleStarted.RemoveListener(AddTimeScale);
        //OnSingleEnded.RemoveListener(RemoveTimeScale);
    } 
    #endregion

    #region Logic

    public void SetTimeScale()
    {
        OnChangeTimeScale?.Invoke(TimeScale);
    }
    
    /// <summary>
    /// Add the TimeScaleInterpolation to the TimeScaleManager.
    /// If the <see cref="TimeScaleManager"/> is null, nothing will happen.
    /// </summary>
    [EasyButtons.Button]
    public void AddTimeScale()
    {
        if(TimeScaleManager.Instance == null)
        {
            return;
        }
        
        TimeScaleManager.Instance.AddTimeScaleable(this);
        SetTimeScale();
    }

    /// <summary>
    /// Remove the TimeScaleInterpolation from the TimeScaleManager.
    /// If the <see cref="TimeScaleManager"/> is null, nothing will happen.
    /// </summary>
    [EasyButtons.Button]
    public void RemoveTimeScale()
    {
        if(TimeScaleManager.Instance == null)
        {
            return;
        }
        
        TimeScaleManager.Instance.RemoveTimeScaleable(this);
    }

    public override void SetValue(float value)
    {
        TimeScale = value;
        SetTimeScale();
    }

    #endregion
}
