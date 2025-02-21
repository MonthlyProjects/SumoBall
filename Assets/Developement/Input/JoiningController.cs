using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class JoiningController : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameState JoiningPlayer;

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        JoiningPlayer.OnGameStateActive += () => { EnableJoining(true); };
        JoiningPlayer.OnGameStateInactive += () => { EnableJoining(false); };
    }

    private void OnDisable()
    {
        JoiningPlayer.OnGameStateActive -= () => { EnableJoining(true); };
        JoiningPlayer.OnGameStateInactive -= () => { EnableJoining(false); };
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
        List<Color> colors = new List<Color>
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.cyan,
            Color.magenta,
            Color.black,
            Color.white,
            Color.gray,
            Color.grey,
            new Color(1f, 0.5f, 0f),   // Orange
            new Color(0.5f, 0f, 0.5f), // Purple
            new Color(0.2f, 0.8f, 0.2f), // Light Green
            new Color(0.9f, 0.1f, 0.1f), // Dark Red
            new Color(0.1f, 0.1f, 0.9f), // Dark Blue
            new Color(0.9f, 0.9f, 0.1f), // Gold
            new Color(0.5f, 0.3f, 0.1f), // Brown
            new Color(0.8f, 0.6f, 1f), // Lavender
            new Color(1f, 0.8f, 0.6f), // Peach
            new Color(0f, 0.5f, 0.5f)  // Teal
        };

        int randomInt = Random.Range(0, colors.Count);

        Multi_InputManager.Instance.OnPlayerAdd(playerInput);

        gameData.AddPlayer(new PlayerData(playerInput, colors[randomInt]));
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
