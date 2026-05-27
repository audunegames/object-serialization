using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.Rect"/> objects.
  /// </summary>
  internal class RectTypeAdapter : ICompoundTypeAdapter<Rect>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.Rect;

    
    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(Rect value)
    {
      return new ValueState[] { new(value.x), new(value.y), new(value.width), new(value.height) };
    }

    /// <inheritdoc/>
    public Rect FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return new Rect((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
    
    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, Rect value)
    {
      value.Set((float)states[0], (float)states[1], (float)states[2], (float)states[3]);
    }
  }
}