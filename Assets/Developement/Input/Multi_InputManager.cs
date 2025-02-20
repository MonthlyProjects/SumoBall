using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Multi_InputManager : MonoBehaviour
{
    public static Multi_InputManager Instance;

    [SerializeField] private GameData gameData;

    [SerializeField] private Transform playerInputParent;
    [SerializeField] private List<PlayerInput> playerInputs;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        playerInputs = new List<PlayerInput>();


        gameData.OnPlayerAdd.AddListener(t => { OnPlayerAdd(t.playerInput); });
        gameData.OnPlayerRemove.AddListener(t => { OnPlayerRemove(t.playerInput); });
    }

    private void OnPlayerAdd(PlayerInput playerInput)
    {
        playerInputs.Add(playerInput);

        playerInput.gameObject.transform.parent = playerInputParent;
        EnablePlayerCurrentActionMap(playerInputs.Count - 1, false);
    }

    private void OnPlayerRemove(PlayerInput playerInput)
    {
        playerInputs.Remove(playerInput);
    }

    [EasyButtons.Button]
    public void EnableAllPlayerCurrentActionMap(bool enable)
    {
        for (int i = 0; i < playerInputs.Count; i++)
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
    public void EnablePlayerCurrentActionMap(int index, bool enable)
    {
        if (enable)
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
        for (int i = 0; i < playerInputs.Count; i++)
        {
            playerInputs[i].SwitchCurrentActionMap(actionMap);
        }
    }

    [EasyButtons.Button]
    public void SwitchPlayerActionMap(int index, string actionMap)
    {
        playerInputs[index].SwitchCurrentActionMap(actionMap);
    }

    [EasyButtons.Button]
    public void DestroyAllPLayerInput()
    {
        for (int i = 0; i < playerInputs.Count; i+=0)
        {
            PlayerInput playerInput = playerInputs[i];
            playerInputs.Remove(playerInput);
            Destroy(playerInput.gameObject);
        }
    }

    [EasyButtons.Button]
    public void DestroyPLayerInput(int index)
    {
        PlayerInput playerInput = playerInputs[index];
        playerInputs.Remove(playerInput);
        Destroy(playerInput.gameObject);
    }

    [EasyButtons.Button]
    public void DebugCurrentActionMap(int index)
    {
        InputActionMap v = playerInputs[index].currentActionMap;
        Debug.Log(v.name);
        Debug.Log(v.enabled);
    }



}
