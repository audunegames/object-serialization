using System;
using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a extension state that contains a byte array value.
  /// </summary>
  public sealed class RawExtensionState : State, IValueState, IEquatable<RawExtensionState>
  {
    /// <summary>
    /// The type of the state.
    /// </summary>
    internal readonly RawExtensionType type;

    /// <summary>
    /// The byte array value of the state.
    /// </summary>
    internal readonly byte[] bytes;


    /// <inheritdoc/>
    object IValueState.value => bytes;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">The type of the state.</param>
    /// <param name="bytes">The byte array value of the state.</param>
    internal RawExtensionState(RawExtensionType type, byte[] bytes)
    {
      this.type = type;
      this.bytes = bytes;
    }
    

    #region Equatable implementation
    /// <inheritdoc/>
    public override bool Equals(object other)
    {
      return Equals(other as RawExtensionState);
    }

    /// <inheritdoc/>
    public bool Equals(RawExtensionState other)
    {
      return other is not null
        && EqualityComparer<RawExtensionType>.Default.Equals(type, other.type)
        && Equals(bytes, other.bytes);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(type, bytes);
    }


    /// <summary>
    /// Return if the specified <see cref="RawExtensionState"/>s are equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="RawExtensionState"/> to compare.</param>
    /// <param name="right">The right <see cref="RawExtensionState"/> to compare.</param>
    /// <returns>If the specified <see cref="RawExtensionState"/>s are equal.</returns>
    public static bool operator ==(RawExtensionState left, RawExtensionState right)
    {
      return Equals(left, right);
    }
    
    /// <summary>
    /// Return if the specified <see cref="RawExtensionState"/>s are not equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="RawExtensionState"/> to compare.</param>
    /// <param name="right">The right <see cref="RawExtensionState"/> to compare.</param>
    /// <returns>If the specified <see cref="RawExtensionState"/>s are equal.</returns>
    public static bool operator !=(RawExtensionState left, RawExtensionState right)
    {
      return !(left == right);
    }
    #endregion

    #region Implicit operators
    /// <summary>
    /// Convert a <see cref="RawExtensionState"/> to a <see cref="System.Byte"/>[] value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator byte[](RawExtensionState state) => state.bytes;
    #endregion
  }
}