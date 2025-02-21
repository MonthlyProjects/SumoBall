using UnityEngine;

public class ScaleInterpolation : FloatInterpolation
{
    [SerializeField] private Transform transformToScale;
    public override void SetValue(float value)
    {
        transformToScale.localScale = value * Vector3.one;
    }
}
