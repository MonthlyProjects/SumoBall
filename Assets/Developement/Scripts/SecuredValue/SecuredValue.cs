using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a value that can be modified by multiple objects in a secured manner.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
[Serializable]
public class SecuredValue<T>
{
    [SerializeField] private List<SecuredValueAdder<T>> modifiers;
    public Action<T> OnValueChanged;
    private T _defaultValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecuredValue{T}"/> class with the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value of the secured value.</param>
    public SecuredValue(T defaultValue)
    {
        _defaultValue = defaultValue;
        modifiers = new List<SecuredValueAdder<T>>();
    }

    /// <summary>
    /// Gets the current value of the secured value, taking into account all modifiers.
    /// </summary>
    /// <returns>The current value of the secured value.</returns>
    public T GetValue()
    {
        if (modifiers.Count == 0)
        {
            return _defaultValue;
        }
        return modifiers[0].Value;
    }

    /// <summary>
    /// Adds a modifier to the secured value.
    /// </summary>
    /// <param name="securedValueAdder">The modifier to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="securedValueAdder"/> is null.</exception>
    public void AddModifier(SecuredValueAdder<T> securedValueAdder)
    {
        if (securedValueAdder == null)
        {
            throw new ArgumentNullException(nameof(securedValueAdder));
        }

        if (!modifiers.Contains(securedValueAdder))
        {
            modifiers.Add(securedValueAdder);
            SortModifiersByPriority();
            OnValueChanged?.Invoke(GetValue());
        }
    }

    /// <summary>
    /// Removes a modifier from the secured value.
    /// </summary>
    /// <param name="securedValueAdder">The modifier to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="securedValueAdder"/> is null.</exception>
    public void RemoveModifier(SecuredValueAdder<T> securedValueAdder)
    {
        if (securedValueAdder == null)
        {
            throw new ArgumentNullException(nameof(securedValueAdder));
        }

        if (modifiers.Contains(securedValueAdder))
        {
            modifiers.Remove(securedValueAdder);
            OnValueChanged?.Invoke(GetValue());
        }
    }

    private void SortModifiersByPriority()
    {
        modifiers.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }

    #region Operators override
    //Override the cast operator
    public static implicit operator T(SecuredValue<T> securedValue)
    {
        return securedValue.GetValue();
    }



    // Override the equality operator
    public static bool operator ==(SecuredValue<T> securedValue, T value)
    {
        return securedValue.GetValue().Equals(value);
    }

    // Override the inequality operator
    public static bool operator !=(SecuredValue<T> securedValue, T value)
    {
        return !securedValue.GetValue().Equals(value);
    }
    // Override the less than operator
    public static bool operator <(SecuredValue<T> securedValue, T value)
    {
        return Comparer<T>.Default.Compare(securedValue.GetValue(), value) < 0;
    }

    // Override the greater than operator
    public static bool operator >(SecuredValue<T> securedValue, T value)
    {
        return Comparer<T>.Default.Compare(securedValue.GetValue(), value) > 0;
    }

    // Override the less than or equal to operator
    public static bool operator <=(SecuredValue<T> securedValue, T value)
    {
        return Comparer<T>.Default.Compare(securedValue.GetValue(), value) <= 0;
    }

    // Override the greater than or equal to operator
    public static bool operator >=(SecuredValue<T> securedValue, T value)
    {
        return Comparer<T>.Default.Compare(securedValue.GetValue(), value) >= 0;
    } 
    #endregion

}

/// <summary>
/// Represents a modifier that can be applied to a secured value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
[Serializable]
public class SecuredValueAdder<T>
{
    /// <summary>
    /// Gets or sets the priority of the modifier : lhe lowest the most priority it has.
    /// </summary>
    [Tooltip("Gets or sets the priority of the modifier : lhe lowest the most priority it has")]
    public int Priority;

    /// <summary>
    /// Gets or sets the value of the modifier.
    /// </summary>
    public T Value; 

    /// <summary>
    /// Initializes a new instance of the <see cref="SecuredValueAdder{T}"/> class with the specified priority and value.
    /// </summary>
    /// <param name="priority">The priority of the modifier.</param>
    /// <param name="value">The value of the modifier.</param>
    public SecuredValueAdder(int priority, T value)
    {
        Priority = priority;
        Value = value;
    }
}