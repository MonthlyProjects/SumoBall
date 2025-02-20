using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerData
{
    //Peut etre null
    [SerializeField] public GameObject playerObject;
    //Ne peut pas etre null
    [SerializeField] public PlayerInput playerInput;
    //Peut etre null
    public PlayerSkin playerSkin;

    public PlayerData (PlayerInput playerInput)
    {
        playerObject = null;
        this.playerInput = playerInput;
        this.playerSkin = new PlayerSkin();
    }

}

public struct PlayerSkin
{
    public GameObject skinPrefab;
}
