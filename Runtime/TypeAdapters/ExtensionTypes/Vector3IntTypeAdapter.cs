using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for Vector3Int objects
  internal class Vector3IntTypeAdapter : ICompoundTypeAdapter<Vector3Int>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.Vector3Int;

    
    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(Vector3Int value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.z) };
    }

    // Convert the specified compound state to a value
    public Vector3Int FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector3Int((int)states[0], (int)states[1], (int)states[2]);
    }
    
    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, Vector3Int value)
    {
      value.Set((int)states[0], (int)states[1], (int)states[2]);
    }
  }
}