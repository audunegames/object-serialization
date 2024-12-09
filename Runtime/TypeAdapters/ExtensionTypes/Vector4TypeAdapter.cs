using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for Vector4 objects
  internal class Vector4TypeAdapter : ICompoundTypeAdapter<Vector4>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.Vector4;

    
    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(Vector4 value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.z), new(value.w) };
    }

    // Convert the specified compound state to a value
    public Vector4 FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector4((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
    
    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, ref Vector4 value)
    {
      value.Set((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
  }
}