using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    
    #region Properties

    [SerializeField] private SO_InputButton[] actionsButton;
    [SerializeField] private SO_InputVector[] actionsVector;

    public static InputManager Instance;

    #endregion

    #region Fields

    public static PlayerControls Input { private set; get; }
    
    public bool IsGamepad { get { return IsInputGamepad(); } }

    #endregion
    
    #region Init
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this);
        Input = new PlayerControls();
        ResetInputs();
        foreach (var action in actionsButton)
        {
            Debug.Log(action.InputName + " : inputname");
        }
    }
    private void OnEnable()
    {
        EnableGameInput();
    }
    private void OnDisable()
    {
        DisableGameInput();
    }
    
    #endregion

    #region Input logic
    private void ResetInputs()
    {
        foreach(SO_InputButton action in actionsButton)
        {
            action.ResetValue();
        }
        foreach (SO_InputVector action in actionsVector)
        {
            action.ResetValue();
        }
    }
    void EnableGameInput()
    {
        DisableGameInput();
        Input.Game.Enable();
        for (int i = 0; i < actionsButton.Length; i++)
        {
            SO_InputButton inputData = actionsButton[i];
            try
            {
                InputAction inputAction = Input.FindAction(inputData.InputName);
                inputAction.performed += ctx => inputData.ChangeValue(true);
                inputAction.canceled += ctx => inputData.ChangeValue(false);
            }
            catch(Exception e)
            {
                Debug.LogError("No Input Asset Created in playercontrols with the name associated in the input data " + inputData.name);
                Debug.LogError(e);
            }
        }

        for (int i = 0; i < actionsVector.Length; i++)
        {
            SO_InputVector inputData = actionsVector[i];
            try
            {
                InputAction inputAction = Input.FindAction(inputData.InputName);
                inputAction.performed += ctx => inputData.ChangeValue(inputAction.ReadValue<Vector2>());
                inputAction.canceled += ctx => inputData.ChangeValue(Vector2.zero);
            }
            catch (Exception e)
            {
                Debug.LogError("No Input Asset Created in playercontrols with the name associated in the input data " + inputData.name);
                Debug.LogError(e);
            }
        }
    }
    
    void DisableGameInput()
    {
        for (int i = 0; i < actionsButton.Length; i++)
        {
            SO_InputButton inputData = actionsButton[i];
            try
            {
                InputAction inputAction = Input.FindAction(inputData.InputName);
                // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
                inputAction.performed -= ctx => inputData.ChangeValue(true);
                // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
                inputAction.canceled -= ctx => inputData.ChangeValue(false);
            }
            catch (Exception e)
            {
                Debug.LogError("No Input Asset Created in PlayerControls with the name associated in the input data " + inputData.name);
                Debug.LogError(e);
            }
        }

        for (int i = 0; i < actionsVector.Length; i++)
        {
            SO_InputVector inputData = actionsVector[i];
            try
            {
                InputAction inputAction = Input.FindAction(inputData.InputName);
                // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
                inputAction.performed -= ctx =>
                {
                    if (inputData != null) inputData.ChangeValue(inputAction.ReadValue<Vector2>());
                };
                // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
                inputAction.canceled -= ctx =>
                {
                    if (inputData != null) inputData.ChangeValue(Vector2.zero);
                };
            }
            catch (Exception e)
            {
                Debug.LogError("No Input Asset Created in playercontrols with the name associated in the input data " + inputData.name);
                Debug.LogError(e);
            }
        }

        Input.Game.Disable();
    }

    //this function sets the active value in the scriptabledatas of every inputs marked as Game
    public void ActiveGameInputs(bool value)
    {
        foreach(SO_InputButton inputButton in actionsButton)
        {
            if (inputButton.inputType == InputType.Game)
            {
                inputButton.IsActive = value;
            }
        }
        foreach (SO_InputVector inputVector in actionsVector)
        {
            if (inputVector.inputType == InputType.Game)
            {
                inputVector.IsActive = value;
            }
        }
    }
    public static bool IsInputGamepad()
    {
        InputDevice lastUsedDevice = null;
        float lastEventTime = 0;
        foreach (var device in InputSystem.devices)
        {
            if (device.lastUpdateTime > lastEventTime)
            {
                lastUsedDevice = device;
                lastEventTime = (float)device.lastUpdateTime;
            }
        }

        return lastUsedDevice is Gamepad;
    } 
    
    #endregion
    
}