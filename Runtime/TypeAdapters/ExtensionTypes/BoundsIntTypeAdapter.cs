using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for BoundsInt objects
  internal class BoundsIntTypeAdapter : ICompoundTypeAdapter<BoundsInt>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType => ExtensionType.BoundsInt;


    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(BoundsInt value)
    {
      return new ValueState[] { new(value.position.x), new(value.position.y), new(value.position.z), new(value.size.x), new(value.size.y), new(value.size.z) };
    }

    // Convert the specified compound state to a value
    public BoundsInt FromCompoundState(IReadOnlyList<ValueState> states)
    {
      var position = new Vector3Int((int)states[0], (int)states[1], (int)states[2]);
      var size = new Vector3Int((int)states[3], (int)states[4], (int)states[5]);
      return new BoundsInt(position, size);
    }
    
    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, ref BoundsInt value)
    {
      value.position.Set((int)states[0], (int)states[1], (int)states[2]);
      value.size.Set((int)states[3], (int)states[4], (int)states[5]);
    }
  }
}