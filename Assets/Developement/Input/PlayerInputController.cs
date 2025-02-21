using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] SO_InputVector sO_InputVector;

    private PlayerInput _playerInput;

    public PlayerInput PlayerInput { get { return _playerInput; } set { _playerInput = value; } }

    public void Initialize(PlayerInput playerInput , SO_InputVector sO_InputVector)
    {
        _playerInput = playerInput;
        this.sO_InputVector = sO_InputVector;

        Initialize();
    }

    public void Initialize ()
    {
        InputAction actionMove = _playerInput.actions["Move"];
        actionMove.performed += ctx => sO_InputVector.ChangeValue(ctx.ReadValue<Vector2>());
        actionMove.canceled += ctx => sO_InputVector.ChangeValue(Vector2.zero);
    }

    public void DeInitialize()
    {
        InputAction actionMove = _playerInput.actions["Move"];
        actionMove.performed += ctx => sO_InputVector.ChangeValue(ctx.ReadValue<Vector2>());
        actionMove.canceled += ctx => sO_InputVector.ChangeValue(Vector2.zero);
    }

    public void EnableBindingMap (bool enable)
    {
        if (enable)
        {
            _playerInput.currentActionMap.Enable();
        }
        else
        {
            _playerInput.currentActionMap.Disable();
        }
    }

    private void SwitchPlayerActionMap(BindingMap bindingMap)
    {
        _playerInput.SwitchCurrentActionMap(nameof(bindingMap));
    }
}
