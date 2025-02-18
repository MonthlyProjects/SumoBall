using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour
{
    [SerializeField] private List<PowerUp> effects;

    public void DistributeEffect<T>() where T : PowerUp
    {
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
