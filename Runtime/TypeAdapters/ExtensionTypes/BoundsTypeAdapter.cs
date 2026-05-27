using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.Bounds"/> objects.
  /// </summary>
  internal class BoundsTypeAdapter : ICompoundTypeAdapter<Bounds>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.Bounds;


    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(Bounds value)
    {
      return new ValueState[] { new(value.center.x), new(value.center.y), new(value.center.z), new(value.size.x), new(value.size.y), new(value.size.z) };
    }

    /// <inheritdoc/>
    public Bounds FromCompoundState(IReadOnlyList<ValueState> states)
    {
      var center = new Vector3((float)states[0], (float)states[1], (float)states[2]);
      var size = new Vector3((float)states[3], (float)states[4], (float)states[5]);
      return new Bounds(center, size);
    }

    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, Bounds value)
    {
      value.center.Set((float)states[0], (float)states[1], (float)states[2]);
      value.size.Set((float)states[3], (float)states[4], (float)states[5]);
    }
  }
}