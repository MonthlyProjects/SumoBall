using UnityEngine;

public class PowerUpScaleUp : PowerUp
{
    [SerializeField] private Transform ballParent;
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        ballParent.localScale = Vector3.one * 2;
    }
    public override void CancelEffect()
    {
        ballParent.localScale = Vector3.one;
    }
}
