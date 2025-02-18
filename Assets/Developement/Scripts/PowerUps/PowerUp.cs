using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public bool IsAppliedToCurrentPlayer;
    public void ApplyEffect()
    {
        Debug.Log(this.name);
    }
}
