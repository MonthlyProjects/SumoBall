using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingInterpolation : FloatInterpolation
{
    [SerializeField] private Volume postProcess;
    public override void SetValue(float value)
    {
        postProcess.weight = value;
    }
    [EasyButtons.Button]
    public void SetLoopContinue(bool value)
    {
        constantLoop = value;
    }
}
