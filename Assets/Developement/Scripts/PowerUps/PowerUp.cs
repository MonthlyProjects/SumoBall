using System.Collections;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public bool IsAppliedToCurrentPlayer;
    [SerializeField] protected float duration;
    public virtual void ApplyEffect()
    {
        StartCoroutine(WaitToCancelEffect());
    }
    IEnumerator WaitToCancelEffect()
    {
        yield return new WaitForSeconds(duration);
        CancelEffect();
    }
    public abstract void CancelEffect();
}
