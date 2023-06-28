using System;
using System.Collections.Generic;
using UnityEngine;

// Declaration of the class "ModifiableInt"
[Serializable]
public class ModifiableInt
{
    [SerializeField]
    private int _baseValue;
    public int BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            UpdateModifiedValue();
        }
    }

    [SerializeField]
    private int _modifiedValue;
    public int ModifiedValue
    {
        get => _modifiedValue;
        private set => _modifiedValue = value;
    }

    // Event triggered whenever the value changes to update other code
    private event Action ValueModified;

    private List<IModifiers> _modifiers = new List<IModifiers>();

    // Constructor for ModifiableInt, can optionally take a method to subscribe to the ValueModified event
    public ModifiableInt(Action method = null)
    {
        ModifiedValue = _baseValue;
        if (method != null)
            ValueModified += method;
    }

    // Method to register a method to be invoked when the ValueModified event is triggered
    public void RegisterModEvent(Action method)
    {
        ValueModified += method;
    }

    // Method to unregister a method from the ValueModified event
    public void UnregisterModEvent(Action method)
    {
        ValueModified -= method;
    }

    // Method to update the modified value based on the base value and applied modifiers
    private void UpdateModifiedValue()
    {
        var valueToAdd = 0;
        for (int i = 0; i < _modifiers.Count; i++)
        {
            _modifiers[i].AddValue(ref valueToAdd);
        }
        ModifiedValue = _baseValue + valueToAdd;
        ValueModified?.Invoke();
    }

    // Method to add a modifier to the list of modifiers and update the modified value
    public void AddModifier(IModifiers modifier)
    {
        _modifiers.Add(modifier);
        UpdateModifiedValue();
    }

    // Method to remove a modifier from the list of modifiers and update the modified value
    public void RemoveModifier(IModifiers modifier)
    {
        _modifiers.Remove(modifier);
        UpdateModifiedValue();
    }
}