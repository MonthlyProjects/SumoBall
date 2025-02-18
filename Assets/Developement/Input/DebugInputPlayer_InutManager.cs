using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DebugInputPlayer_InutManager : MonoBehaviour
{
    [SerializeField] private List<PlayerInput> playerInputs;
    [SerializeField] private PlayerInputManager playerInputManager;

    private void Start()
    {
        playerInputManager.playerJoinedEvent.AddListener(AddPlayer);

        //Appeler lorsque le perso est detruit 
        playerInputManager.playerLeftEvent.AddListener(RemovePlayer);
    }

    public void AddPlayer (PlayerInput playerInput)
    {
        Debug.Log("Player join" + playerInput.name);

        playerInput.deviceLostEvent.AddListener((playerInput) =>
        {
            Debug.Log("Device was lost of player" + playerInput.name);
        });

        playerInput.deviceRegainedEvent.AddListener((playerInput) =>
        {
            Debug.Log("Regained device of player" + playerInput.name);
        });

        playerInput.controlsChangedEvent.AddListener((playerInput) =>
        {
            Debug.Log("Control change of player" + playerInput.name);
        });

        playerInputs.Add(playerInput);
    }

    public void RemovePlayer (PlayerInput playerInput)
    {
        Debug.Log("Player left" + playerInput.name);

        playerInputs.Remove(playerInput);
    }

}
