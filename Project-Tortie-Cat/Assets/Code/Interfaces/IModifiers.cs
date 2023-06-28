using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declaration of an interface called "IModifiers"
public interface IModifiers
{
    // Declaration of a method called "AddValue" that takes a reference to an integer parameter named "baseValue"
    // The "ref" keyword indicates that the parameter is passed by reference
    // This means that any changes made to the "baseValue" parameter inside the method will also affect the original variable passed as an argument
    void AddValue(ref int baseValue);
}