using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for Vector2Int objects
  internal class Vector2IntTypeAdapter : ICompoundTypeAdapter<Vector2Int>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.Vector2Int;

    
    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(Vector2Int value)
    {
      return new ValueState[] { new(value.x), new(value.y) };
    }

    // Convert the specified compound state to a value
    public Vector2Int FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector2Int((int)states[0], (int)states[1]);
    }
    
    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, Vector2Int value)
    {
      value.Set((int)states[0], (int)states[1]);
    }
  }
}