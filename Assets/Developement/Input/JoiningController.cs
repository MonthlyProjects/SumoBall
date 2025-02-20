using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class JoiningController : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _playerInputManager.playerJoinedEvent.AddListener(OnPlayerJoin);
        _playerInputManager.playerLeftEvent.AddListener(OnPlayerLeft);
    }

    /// <summary>
    /// Save PlayerInput in the GameData
    /// </summary>
    /// <param name="playerInput"></param>
    private void OnPlayerJoin(PlayerInput playerInput)
    {
        gameData.AddPlayer(new PlayerData(playerInput));
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        gameData.RemovePlayer(playerInput.playerIndex);
    }
}
