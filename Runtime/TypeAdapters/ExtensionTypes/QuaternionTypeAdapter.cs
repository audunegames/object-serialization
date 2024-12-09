using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines a type adapter for Quaternion objects
  internal class QuaternionTypeAdapter : ICompoundTypeAdapter<Quaternion>
  {
    // The extension type of the type adapter
    public CompoundExtensionType extensionType=> ExtensionType.Quaternion;
    

    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(Quaternion value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.z), new(value.w) };
    }

    // Convert the specified compound state to a value
    public Quaternion FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Quaternion((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
    
    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, ref Quaternion value)
    {
      value.Set((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
  }
}