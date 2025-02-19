using UnityEngine;

public class PowerUpScaleUp : PowerUp
{
    [SerializeField] private Transform ballParent;
    [SerializeField] private BallController ballController;
    [SerializeField] private float multiplierForce = 3f;
    [SerializeField] private float multiplierScale = 1.5f;

    private float oldForce;
    [EasyButtons.Button]
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        ballParent.localScale = Vector3.one * multiplierScale;
        oldForce = ballController.PushForce;
        ballController.PushForce *= multiplierForce;
    }
    public override void CancelEffect()
    {
        ballParent.localScale = Vector3.one;
        ballController.PushForce = oldForce;
    }
}
