using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public enum InputType
{
    Game,
    UI
}
public abstract class SO_Input<T> : ScriptableObject
{
    public event Action<T> OnValueChanged;
    [SerializeField] bool isActive = true;
    public bool IsActive { get { return isActive; } set { if (!value) { ChangeValue(default); } isActive = value; } }
    public T Value;
    public void ChangeValue(T value)
    {
        if (isActive)
        {
            OnValueChanged?.Invoke(value);
            this.Value = value;
        }
    }
    public void ResetValue()
    {
        Value = default;
    }
    [SerializeField, HideInInspector] private string inputName = "";
    public InputActionAsset inputActionAsset;
    public string InputName { get { return inputName; } set { inputName = value; } }
    public InputType inputType = InputType.Game;
}
