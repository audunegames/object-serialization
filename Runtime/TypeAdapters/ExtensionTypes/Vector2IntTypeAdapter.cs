using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.Vector2Int"/> objects.
  /// </summary>
  internal class Vector2IntTypeAdapter : ICompoundTypeAdapter<Vector2Int>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.Vector2Int;

    
    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(Vector2Int value)
    {
      return new ValueState[] { new(value.x), new(value.y) };
    }

    /// <inheritdoc/>
    public Vector2Int FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Vector2Int((int)states[0], (int)states[1]);
    }
    
    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, Vector2Int value)
    {
      value.Set((int)states[0], (int)states[1]);
    }
  }
}