using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class JoiningController : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    public UnityEvent<PlayerData> OnPlayerAdd;
    public UnityEvent<PlayerData> OnPlayerRemove;

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();

        _playerInputManager.playerJoinedEvent.AddListener(OnPlayerJoin);
    }

    /// <summary>
    /// Save PlayerInput in the GameData
    /// </summary>
    /// <param name="playerInput"></param>
    private void OnPlayerJoin(PlayerInput playerInput)
    {
        gameData.AddPlayer(new PlayerData(playerInput));
        OnPlayerAdd?.Invoke(gameData.playersData[gameData.playersData.Count - 1]);
    }

    //Determine quand l appeler
    private void OnPlayerLeft(PlayerInput playerInput)
    {
        gameData.RemovePlayer(playerInput.playerIndex);
    }


    [EasyButtons.Button]
    public void EnableJoining(bool enable)
    {
        if (enable)
        {
            _playerInputManager.EnableJoining();
        }
        else
        {
            _playerInputManager.DisableJoining();
        }
    }
}
