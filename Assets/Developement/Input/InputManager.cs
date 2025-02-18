using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField] private Transform playerInputParent;
    [SerializeField] private List<PlayerInput> playerInputs;

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize ()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        playerInputs = new List<PlayerInput>();

        _playerInputManager = gameObject.GetComponent<PlayerInputManager>();

        _playerInputManager.playerJoinedEvent.AddListener(OnPlayerJoin);
        _playerInputManager.playerLeftEvent.AddListener(OnPlayerLeft);
    }

    [EasyButtons.Button]
    public void EnableJoining (bool enable)
    {
        if(enable)
        {
            _playerInputManager.EnableJoining();
        }
        else
        {
            _playerInputManager.DisableJoining();
        }
    }

    private void OnPlayerJoin (PlayerInput playerInput)
    {
        playerInputs.Add(playerInput);

        playerInput.gameObject.transform.parent = playerInputParent;
        EnablePlayerCurrentActionMap(playerInputs.Count - 1, false);
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        playerInputs.Remove(playerInput);
    }

    [EasyButtons.Button]
    public void EnableAllPlayerCurrentActionMap(bool enable)
    {
        for(int i = 0; i < playerInputs.Count; i++)
        {
            if (enable)
            {
                playerInputs[i].currentActionMap.Enable();
            }
            else
            {
                playerInputs[i].currentActionMap.Disable();
            }
        }
    }

    [EasyButtons.Button]
    public void EnablePlayerCurrentActionMap (int index, bool enable)
    {
        //CheckIndex
        if(enable)
        {
            playerInputs[index].currentActionMap.Enable();
        }
        else
        {
            playerInputs[index].currentActionMap.Disable();
        }
    }

    [EasyButtons.Button]
    public void SwitchPlayerActionMap(string actionMap)
    {
        for(int i =0; i < playerInputs.Count;i++)
        {
            playerInputs[i].SwitchCurrentActionMap(actionMap);
        }
    }

    [EasyButtons.Button]
    public void SwitchPlayerActionMap (int index, string actionMap)
    {
        playerInputs[index].SwitchCurrentActionMap(actionMap);
    }

    [EasyButtons.Button]
    public void DestroyPLayerInput (int index)
    {
        PlayerInput playerInput = playerInputs[index];
        playerInputs.Remove(playerInput);
        Destroy(playerInput.gameObject);
    }

    [EasyButtons.Button]
    public void DebugCurrentActionMap (int index)
    {
        InputActionMap v = playerInputs[index].currentActionMap;
        Debug.Log(v.name);
        Debug.Log(v.enabled);
    }
}
