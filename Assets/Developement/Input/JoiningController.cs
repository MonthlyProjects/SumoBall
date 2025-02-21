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
        List<Color> colors = new List<Color>();
        colors.Add(Color.yellow);
        colors.Add(Color.red);
        colors.Add(Color.green);
        colors.Add(Color.blue);
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
