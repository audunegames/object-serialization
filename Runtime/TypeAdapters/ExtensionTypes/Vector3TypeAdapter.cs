using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.Vector3"/> objects.
  /// </summary>
  internal class Vector3TypeAdapter : ICompoundTypeAdapter<Vector3>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.Vector3;

    
    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(Vector3 value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.z) };
    }

    /// <inheritdoc/>
    public Vector3 FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector3((float)states[0], (float)states[1], (float)states[2]);
    }
    
    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, Vector3 value)
    {
      value.Set((float)states[0], (float)states[1], (float)states[2]);
    }
  }
}