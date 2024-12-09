using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Serialization
{
  // Class that defines a compound extension state
  public class CompoundExtensionState : State, IEquatable<CompoundExtensionState>
  {
    // The type of the compound state
    internal readonly CompoundExtensionType type;

    // The states of the compound state
    internal readonly ValueState[] states;


    // Constructor
    internal CompoundExtensionState(CompoundExtensionType type, IReadOnlyList<ValueState> states)
    {
      this.type = type;
      this.states = states is ValueState[] statesArray ? statesArray : states.ToArray();

      type.AssertFields(this.states);
    }


    #region Equatable implementation
    // Return if the state equals another object
    public override bool Equals(object other)
    {
      return Equals(other as CompoundExtensionState);
    }

    // Return if the state equals another state
    public bool Equals(CompoundExtensionState other)
    {
      return other is not null
        && EqualityComparer<ExtensionType>.Default.Equals(type, other.type)
        && EqualityComparer<State[]>.Default.Equals(states, other.states);
    }

    // Return the hash code of the boolean
    public override int GetHashCode()
    {
      return HashCode.Combine(type, states);
    }

    public IEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }


    // Return if the state equals another state using the equal operator
    public static bool operator ==(CompoundExtensionState left, CompoundExtensionState right)
    {
      return Equals(left, right);
    }

    // Return if the state does not equal another state using the not equal operator
    public static bool operator !=(CompoundExtensionState left, CompoundExtensionState right)
    {
      return !(left == right);
    }
    #endregion
  }
}