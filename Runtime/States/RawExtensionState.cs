using System;
using System.Collections.Generic;

namespace Audune.Pickle
{
  // Class that defines a raw extension state
  public sealed class RawExtensionState : State, IValueState, IEquatable<RawExtensionState>
  {
    // The type of the raw extension state
    internal readonly RawExtensionType type;

    // The bytes of the raw extension state
    internal byte[] bytes;


    // Return the value of the raw extension state
    object IValueState.value => bytes;


    // Constructor
    internal RawExtensionState(RawExtensionType type, byte[] bytes)
    {
      this.type = type;
      this.bytes = bytes;
    }
    

    #region Equatable implementation
    // Return if the state equals another object
    public override bool Equals(object other)
    {
      return Equals(other as ValueState);
    }

    // Return if the state equals another state
    public bool Equals(RawExtensionState other)
    {
      return other is not null
        && EqualityComparer<RawExtensionType>.Default.Equals(type, other.type)
        && Equals(bytes, other.bytes);
    }

    // Return the hash code of the state
    public override int GetHashCode()
    {
      return HashCode.Combine(type, bytes);
    }


    // Return if the state equals another state using the equal operator
    public static bool operator ==(RawExtensionState left, RawExtensionState right)
    {
      return Equals(left, right);
    }
    
    // Return if the state does not equal another state using the not equal operator
    public static bool operator !=(RawExtensionState left, RawExtensionState right)
    {
      return !(left == right);
    }
    #endregion

    #region Implicit operators
    // Operators that convert from states to span values
    public static implicit operator byte[](RawExtensionState state) => state.bytes;
    #endregion
  }
}