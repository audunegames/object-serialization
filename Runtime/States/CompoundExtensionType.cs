using System;
using System.Collections.Generic;

namespace Audune.Pickle
{
  // Class that defines an extension type that matches a compound value state
  public class CompoundExtensionType : ExtensionType
  {
    // The value types of the compound value extension type
    private readonly Type[] _valueTypes;


    // Return the value types of the compound value extension type
    internal IReadOnlyList<Type> valueTypes => _valueTypes;


    // Constructor
    public CompoundExtensionType(sbyte code, params Type[] valueTypes) : base(code)
    {
      _valueTypes = valueTypes;
    }

    // Constructor that repeats the specified type for the specified count
    public CompoundExtensionType(sbyte code, Type valueType, int count) : base(code)
    {
      _valueTypes = new Type[count];
      Array.Fill(_valueTypes, valueType);
    }


    // Return if an array of value states matches the component type
    internal void AssertMatches(params ValueState[] states)
    {
      // Check if the lengths match
      if (_valueTypes.Length != states.Length)
        throw new StateException($"Expected {_valueTypes.Length} states, but got {states.Length}");
      
      // Iterate over the states
      for (int i = 0; i < states.Length; i ++)
      {
        // Check if the types match
        if (!states[i].value.TryCast(_valueTypes[i]).IsSuccesful())
          throw new StateException($"Expected value type of {_valueTypes[i]} at index {i}, but got {states[i].value.GetType()}");
      }
    }
  }
}