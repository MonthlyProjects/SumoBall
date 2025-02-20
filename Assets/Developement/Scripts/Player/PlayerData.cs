using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData
{
    //Peut etre null
    public GameObject playerObject;
    //Ne peut pas etre null
    public PlayerInput playerInput;
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
