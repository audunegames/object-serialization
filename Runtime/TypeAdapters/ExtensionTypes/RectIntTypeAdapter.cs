using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.RectInt"/> objects.
  /// </summary>
  internal class RectIntTypeAdapter : ICompoundTypeAdapter<RectInt>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.RectInt;

    
    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(RectInt value)
    {
      return new ValueState[] {new(value.x), new(value.y), new(value.width), new(value.height) };
    }

    /// <inheritdoc/>
    public RectInt FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new RectInt((int)states[0], (int)states[1], (int)states[2], (int)states[3]);
    }

    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, RectInt value)
    {
      value.position.Set((int)states[0], (int)states[1]);
      value.size.Set((int)states[2], (int)states[3]);
    }
  }
}