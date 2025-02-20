using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPowerUpController : MonoBehaviour
{
    [SerializeField] private List<PowerUp> effects;
    public UnityEvent OnPowerUpPickedUp;

    public void DistributeEffect<T>() where T : PowerUp
    {
        OnPowerUpPickedUp?.Invoke();
        PowerUp effectToUse = GetPowerUp<T>();
        if(effectToUse.IsAppliedToCurrentPlayer)
        {
            effectToUse.ApplyEffect();
            Debug.Log("effect Used By This Player");
        }
        else
        {
            //for all other players
            //playerPowerUpController.GetPowerUp<T>().ApplyEffect()

            Debug.Log("effect Used By Other Players");
        }
    }


    public PowerUp GetPowerUp<T>()
    {
        return effects.Find(e => e is T);
    }
}
