using System;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an extension state that contains an array of value states.
  /// </summary> 
  public class CompoundExtensionState : State, IEquatable<CompoundExtensionState>
  {
    /// <summary>
    /// The type of the state.
    /// </summary>
    internal readonly CompoundExtensionType type;

    /// <summary>
    /// The array of value states of the state.
    /// </summary>
    internal readonly ValueState[] states;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">The type of the state.</param>
    /// <param name="states">The array of value states of the state.</param>
    internal CompoundExtensionState(CompoundExtensionType type, IReadOnlyList<ValueState> states)
    {
      this.type = type;
      this.states = states as ValueState[] ?? states.ToArray();

      type.AssertFields(this.states);
    }


    #region Equatable implementation
    /// <inheritdoc/>
    public override bool Equals(object other)
    {
      return Equals(other as CompoundExtensionState);
    }

    /// <inheritdoc/>
    public bool Equals(CompoundExtensionState other)
    {
      return other is not null
        && EqualityComparer<ExtensionType>.Default.Equals(type, other.type)
        && EqualityComparer<ValueState[]>.Default.Equals(states, other.states);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(type, states);
    }


    /// <summary>
    /// Return if the specified <see cref="CompoundExtensionState"/>s are equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="CompoundExtensionState"/> to compare.</param>
    /// <param name="right">The right <see cref="CompoundExtensionState"/> to compare.</param>
    /// <returns>If the specified <see cref="CompoundExtensionState"/>s are equal.</returns>
    public static bool operator ==(CompoundExtensionState left, CompoundExtensionState right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Return if the specified <see cref="CompoundExtensionState"/>s are not equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="CompoundExtensionState"/> to compare.</param>
    /// <param name="right">The right <see cref="CompoundExtensionState"/> to compare.</param>
    /// <returns>If the specified <see cref="CompoundExtensionState"/>s are equal.</returns>
    public static bool operator !=(CompoundExtensionState left, CompoundExtensionState right)
    {
      return !(left == right);
    }
    #endregion
  }
}