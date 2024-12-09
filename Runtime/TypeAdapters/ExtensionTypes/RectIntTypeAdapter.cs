using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for RectInt objects
  internal class RectIntTypeAdapter : ICompoundTypeAdapter<RectInt>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.RectInt;

    
    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(RectInt value)
    {
      return new ValueState[] {new(value.x), new(value.y), new(value.width), new(value.height) };
    }

    // Convert the specified compound state to a value
    public RectInt FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new RectInt((int)states[0], (int)states[1], (int)states[2], (int)states[3]);
    }

    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, ref RectInt value)
    {
      value.position.Set((int)states[0], (int)states[1]);
      value.size.Set((int)states[2], (int)states[3]);
    }
  }
}