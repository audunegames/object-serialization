using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.Vector2"/> objects.
  /// </summary>
  internal class Vector2TypeAdapter : ICompoundTypeAdapter<Vector2>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.Vector2;

    
    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(Vector2 value)
    {
      return new ValueState[] { new(value.x), new(value.y) };
    }

    /// <inheritdoc/>
    public Vector2 FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector2((float)states[0], (float)states[1]);
    }
    
    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, Vector2 value)
    {
      value.Set((float)states[0], (float)states[1]);
    }
  }
}