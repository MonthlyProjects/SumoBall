using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    #region Fields

    public static TimeScaleManager Instance;

    [SerializeField] private List<ITimeScaleable> currentTimeScaleModifiers;
    [SerializeField] private List<GameObject> timescaleModifiers; //Only for debuging
    
    #endregion

    #region Init
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        currentTimeScaleModifiers = new List<ITimeScaleable>();
    }
    #endregion

    #region Methods
    public void AddTimeScaleable(ITimeScaleable timeScaleable)
    {
        if( currentTimeScaleModifiers.Contains(timeScaleable))
        {
            return;
        }

        currentTimeScaleModifiers.Add(timeScaleable);
        SortTimeScaleableByPriority();
        currentTimeScaleModifiers[0].OnChangeTimeScale += SetTimeScale;
    }

    public void RemoveTimeScaleable(ITimeScaleable timeScaleable)
    {
        if(!currentTimeScaleModifiers.Contains(timeScaleable))
        {
            return;
        }

        if (currentTimeScaleModifiers.Count > 0 && currentTimeScaleModifiers[0] == timeScaleable)
        {
            currentTimeScaleModifiers[0].OnChangeTimeScale -= SetTimeScale;
        }

        currentTimeScaleModifiers.Remove(timeScaleable);
        timescaleModifiers.Remove(timeScaleable.OwnerObject);

        if (currentTimeScaleModifiers.Count > 0)
        {
            SetTimeScale(currentTimeScaleModifiers[0].TimeScale);
            currentTimeScaleModifiers[0].OnChangeTimeScale += SetTimeScale;
        }
        else
        {
            ResetTimeScale();
        }
    }
    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
    private void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
    private void SortTimeScaleableByPriority()
    {
        currentTimeScaleModifiers.Sort((a, b) => a.Priority.CompareTo(b.Priority));

        timescaleModifiers = new();
        for(int i = 0; i < currentTimeScaleModifiers.Count; i++)
        {
            timescaleModifiers.Add(currentTimeScaleModifiers[currentTimeScaleModifiers.Count - 1 - i].OwnerObject);
        }
    } 
    #endregion
}
