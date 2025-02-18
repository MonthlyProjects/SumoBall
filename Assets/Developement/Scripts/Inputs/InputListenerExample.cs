using UnityEngine;
using UnityEngine.Events;

public class InputListenerExample : MonoBehaviour
{
    [SerializeField] private SO_InputButton _inputButton;
    [SerializeField] private SO_InputVector _inputVector;
    public UnityEvent<bool> onButtonPressed;
    public UnityEvent<Vector2> onVectorMoved;

    private void OnEnable()
    {
        _inputButton.OnValueChanged += ButtonMethod;
        _inputVector.OnValueChanged += VectorMethod;
    }
    private void OnDisable()
    {
        _inputButton.OnValueChanged -= ButtonMethod;
        _inputVector.OnValueChanged -= VectorMethod;
    }
    private void ButtonMethod(bool value)
    {
        onButtonPressed?.Invoke(value);
    }
    private void VectorMethod(Vector2 value)
    {
        onVectorMoved?.Invoke(value);
    }
}
