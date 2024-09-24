using System.Collections.Generic;
using UnityEngine;

namespace Audune.Pickle
{
  // Class that defines a type adapter for Rect objects
  internal class RectTypeAdapter : ICompoundTypeAdapter<Rect>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.Rect;

    
    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(Rect value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.width), new(value.height) };
    }

    // Convert the specified compound state to a value
    public Rect FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Rect((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
    
    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, ref Rect value)
    {
      value.Set((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
  }
}