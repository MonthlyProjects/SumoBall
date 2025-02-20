using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInstanceur : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerSkin defaultSkin;

    [SerializeField] private Transform playerParent;

    [SerializeField] private List<Transform> sps;

    public UnityEvent<PlayerData> OnPlayerSpawn;
    public UnityEvent<PlayerData> OnPlayerDespawn;


    [EasyButtons.Button]
    public void InstancePlayers ()
    {
        //Check si l object
        for (int i = 0; i < gameData.playersData.Count; i++)
        {
            if (gameData.playersData[i].playerObject != null) { continue; }

            PlayerSkin playerSkin = gameData.playersData[i].playerSkin;

            if (playerSkin.skinPrefab == null)
            {
                playerSkin = defaultSkin;
            } 

            gameData.playersData[i].playerObject = Instantiate(playerPrefab, sps[Random.Range(0,sps.Count)].position, Quaternion.identity, playerParent);

            gameData.playersData[i].playerObject.GetComponent<PlayerController>().InitializePlayer(new PlayerController.InitializeData()
            {
                PlayerInput = gameData.playersData[i].playerInput,
                PlayerSkin = playerSkin
            });

            OnPlayerSpawn.Invoke(gameData.playersData[i]);
        }
    }

    public void InstancePlayer (PlayerData playerData)
    {
        if (playerData.playerObject != null) { return; }

        PlayerSkin playerSkin = playerData.playerSkin;

        if (playerSkin.skinPrefab == null)
        {
            playerSkin = defaultSkin;
        }

        playerData.playerObject = Instantiate(playerPrefab, sps[Random.Range(0, sps.Count)].position, Quaternion.identity, playerParent);

        playerData.playerObject.GetComponent<PlayerController>().InitializePlayer(new PlayerController.InitializeData()
        {
            PlayerInput = playerData.playerInput,
            PlayerSkin = playerSkin
        });

        OnPlayerSpawn.Invoke(playerData);
    }

    [EasyButtons.Button]
    public void DestroyPlayers ()
    {
        if(gameData.playersData == null) { return; }

        for(int i = 0; i < gameData.playersData.Count; i++)
        {
            if (gameData.playersData[i].playerObject == null) { continue; }

            GameObject player = gameData.playersData[i].playerObject;

            player.GetComponent<PlayerController>().DestroyPlayer();

            gameData.playersData[i].playerObject = null;

            OnPlayerDespawn.Invoke(gameData.playersData[i]);
        }
    }

    [EasyButtons.Button]
    public void DestroyPlayers(PlayerData playerData)
    {
        if (playerData != null) { return; }

        GameObject player = playerData.playerObject;

        player.GetComponent<PlayerController>().DestroyPlayer();

        playerData.playerObject = null;

        OnPlayerDespawn.Invoke(playerData);
    }
}
