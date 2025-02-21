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

    public PlayerData (PlayerInput playerInput, Color color)
    {
        playerObject = null;
        this.playerInput = playerInput;
        this.playerSkin = new PlayerSkin() { 
        color = color
        };
    }

}

public class PlayerSkin
{
    public GameObject skinPrefab;
    public Color color;
}
