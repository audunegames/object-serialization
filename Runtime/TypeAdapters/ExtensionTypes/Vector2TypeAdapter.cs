using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for Vector2 objects
  internal class Vector2TypeAdapter : ICompoundTypeAdapter<Vector2>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.Vector2;

    
    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(Vector2 value)
    {
      return new ValueState[] { new(value.x), new(value.y) };
    }

    // Convert the specified compound state to a value
    public Vector2 FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector2((float)states[0], (float)states[1]);
    }
    
    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, Vector2 value)
    {
      value.Set((float)states[0], (float)states[1]);
    }
  }
}