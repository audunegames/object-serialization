using System;
using System.Collections.Generic;

namespace Audune.Pickle
{
  // Class that defines a raw state
  public sealed class RawState : State, IEquatable<RawState>
  {
    // The extension type of the raw state
    private RawExtensionType _extensionType;

    // The bytes of the raw state
    private byte[] _bytes;


    // Return the extension type of the raw state
    internal RawExtensionType extensionType => _extensionType;

    // Return the bytes of the raw state
    public byte[] bytes => _bytes;


    // Constructor
    internal RawState(RawExtensionType extensionType, byte[] bytes)
    {
      _extensionType = extensionType;
      _bytes = bytes;
    }

    #region Returning states
    // TODO
    #endregion

    #region Equatable implementation
    // Return if the state equals another object
    public override bool Equals(object other)
    {
      return Equals(other as ValueState);
    }

    // Return if the state equals another state
    public bool Equals(RawState other)
    {
      return other is not null
        && EqualityComparer<RawExtensionType>.Default.Equals(_extensionType, other._extensionType)
        && Equals(_bytes, other._bytes);
    }

    // Return the hash code of the state
    public override int GetHashCode()
    {
      return HashCode.Combine(_extensionType, _bytes);
    }


    // Return if the state equals another state using the equal operator
    public static bool operator ==(RawState left, RawState right)
    {
      return Equals(left, right);
    }
    
    // Return if the state does not equal another state using the not equal operator
    public static bool operator !=(RawState left, RawState right)
    {
      return !(left == right);
    }
    #endregion

    #region Implicit operators
    // Operators that convert from states to span values
    public static implicit operator byte[](RawState state) => state.bytes;
    #endregion
  }
}