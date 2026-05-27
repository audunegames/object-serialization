using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.BoundsInt"/> objects.
  /// </summary>
  internal class BoundsIntTypeAdapter : ICompoundTypeAdapter<BoundsInt>
  {
    /// <inheritdoc/>
    public CompoundExtensionType extensionType => ExtensionType.BoundsInt;


    /// <inheritdoc/>
    public IReadOnlyList<ValueState> ToCompoundState(BoundsInt value)
    {
      return new ValueState[] { new(value.position.x), new(value.position.y), new(value.position.z), new(value.size.x), new(value.size.y), new(value.size.z) };
    }

    /// <inheritdoc/>
    public BoundsInt FromCompoundState(IReadOnlyList<ValueState> states)
    {
      var position = new Vector3Int((int)states[0], (int)states[1], (int)states[2]);
      var size = new Vector3Int((int)states[3], (int)states[4], (int)states[5]);
      return new BoundsInt(position, size);
    }
    
    /// <inheritdoc/>
    public void FromCompoundState(IReadOnlyList<ValueState> states, BoundsInt value)
    {
      value.position.Set((int)states[0], (int)states[1], (int)states[2]);
      value.size.Set((int)states[3], (int)states[4], (int)states[5]);
    }
  }
}