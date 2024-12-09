using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for Bounds objects
  internal class BoundsTypeAdapter : ICompoundTypeAdapter<Bounds>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.Bounds;


    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(Bounds value)
    {
      return new ValueState[] { new(value.center.x), new(value.center.y), new(value.center.z), new(value.size.x), new(value.size.y), new(value.size.z) };
    }

    // Convert the specified compound state to a value
    public Bounds FromCompoundState(IReadOnlyList<ValueState> states)
    {
      var center = new Vector3((float)states[0], (float)states[1], (float)states[2]);
      var size = new Vector3((float)states[3], (float)states[4], (float)states[5]);
      return new Bounds(center, size);
    }

    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, Bounds value)
    {
      value.center.Set((float)states[0], (float)states[1], (float)states[2]);
      value.size.Set((float)states[3], (float)states[4], (float)states[5]);
    }
  }
}