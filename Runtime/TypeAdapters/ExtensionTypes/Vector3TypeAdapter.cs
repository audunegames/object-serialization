using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for Vector3 objects
  internal class Vector3TypeAdapter : ICompoundTypeAdapter<Vector3>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.Vector3;

    
    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(Vector3 value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.z) };
    }

    // Convert the specified compound state to a value
    public Vector3 FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector3((float)states[0], (float)states[1], (float)states[2]);
    }
    
    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, ref Vector3 value)
    {
      value.Set((float)states[0], (float)states[1], (float)states[2]);
    }
  }
}