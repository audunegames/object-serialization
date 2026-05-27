using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.Quaternion"/> objects.
  /// </summary>
  internal class QuaternionTypeAdapter : ICompoundTypeAdapter<Quaternion>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.Quaternion;
    

    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(Quaternion value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.z), new(value.w) };
    }

    /// <inheritdoc/>
    public Quaternion FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Quaternion((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
    
    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, Quaternion value)
    {
      value.Set((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
  }
}