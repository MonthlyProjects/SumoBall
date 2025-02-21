using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInstanceur : MonoBehaviour
{
    [SerializeField] private bool isGame;
    [SerializeField] private GameState loadingRound;

    [SerializeField] private GameData gameData;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerSkin defaultSkin;

    [SerializeField] private Transform playerParent;

    [SerializeField] private List<Transform> spawnPoints;
    private List<Transform> _spawnPointsUse;

    private void OnEnable()
    {
        loadingRound.OnGameStateActive += InstancePlayers;
        gameData.OnPlayerAdd.AddListener((playerData) => { if (!isGame) { InstancePlayer(playerData); } });
    }
    private void OnDisable()
    {
        loadingRound.OnGameStateActive -= InstancePlayers;
        gameData.OnPlayerAdd.RemoveListener((playerData) => { if (!isGame) { InstancePlayer(playerData); } });
    }

    [EasyButtons.Button]
    public void InstancePlayers()
    {
        for (int i = 0; i < gameData.playersData.Count; i++)
        {
            InstancePlayer(gameData.playersData[i]);
        }

        Debug.Log("J ai spawn tout les joueur");

        gameData.OnAllPlayersSpawn?.Invoke();
    }

    private Vector3 ChoseSpawnPosition ()
    {
        if(spawnPoints.Count == 0)
        {
            if(_spawnPointsUse == null || _spawnPointsUse.Count == 0)
            {
                return Vector3.zero;
            }

            for(int i = 0; i < _spawnPointsUse.Count; i+=0)
            {
                spawnPoints.Add(_spawnPointsUse[i]);
                _spawnPointsUse.Remove(_spawnPointsUse[i]);
            }
        }

        Transform transform = spawnPoints[Random.Range(0, spawnPoints.Count)];

        spawnPoints.Remove(transform);
        _spawnPointsUse.Add(transform);

        return transform.position;
    }

    public void InstancePlayer (PlayerData playerData)
    {
        if (playerData.playerObject != null) { return; }
        PlayerSkin playerSkin = playerData.playerSkin;

        playerData.playerObject = Instantiate(playerPrefab, ChoseSpawnPosition(), Quaternion.identity, playerParent);

        playerData.playerObject.GetComponent<PlayerController>().InitializePlayer(new PlayerController.InitializeData()
        {
            PlayerInput = playerData.playerInput,
            PlayerSkin = playerSkin
        });

        gameData.OnPlayerSpawn?.Invoke(playerData);
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
        }
    }

    [EasyButtons.Button]
    public void DestroyPlayers(PlayerData playerData)
    {
        if (playerData != null) { return; }

        GameObject player = playerData.playerObject;

        player.GetComponent<PlayerController>().DestroyPlayer();

        playerData.playerObject = null;
    }
}
