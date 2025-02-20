using System;
using UnityEngine;

public class TimeScaleModifier : MonoBehaviour, ITimeScaleable
{
    #region Fields
    [SerializeField] private float timeScale = 1f;
    [SerializeField] private float priority = 10f;
    
    Action<float> _onChangeTimeScale;
    #endregion

    #region Properties
    public float Priority { get { return priority; } }
    public float TimeScale { get { return timeScale; } set { timeScale = value; } }
    public Action<float> OnChangeTimeScale { get { return _onChangeTimeScale; } set { _onChangeTimeScale = value; } }

    public GameObject OwnerObject => this.gameObject;

    #endregion

    #region MonoBehaviour

    private void OnDisable()
    {
        RemoveTimeScale();
    } 
    #endregion

    #region Logic

    public void SetTimeScale()
    {
        OnChangeTimeScale?.Invoke(TimeScale);
    }
    
    /// <summary>
    /// Add the TimeScaleModifier to the TimeScaleManager.
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
    /// Remove the TimeScaleModifier from the TimeScaleManager.
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

    #endregion
}
