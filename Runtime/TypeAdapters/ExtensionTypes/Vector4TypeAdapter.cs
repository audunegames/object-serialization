using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.Vector4"/> objects.
  /// </summary>
  internal class Vector4TypeAdapter : ICompoundTypeAdapter<Vector4>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.Vector4;

    
    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(Vector4 value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.z), new(value.w) };
    }

    /// <inheritdoc/>
    public Vector4 FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector4((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
    
    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, Vector4 value)
    {
      value.Set((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
  }
}