using System;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Pickle
{
  // Class that defines a compound state
  public class CompoundState : State, IEquatable<CompoundState>
  {
    // The type of the compound state
    private readonly CompoundExtensionType _type;

    // The states of the compound state
    private readonly ValueState[] _states;


    // Return the type of the compound state
    internal ExtensionType type => _type;

    // Return the states of the compound state
    internal IReadOnlyList<ValueState> states => _states;


    // Constructor
    internal CompoundState(CompoundExtensionType type, IReadOnlyList<ValueState> states)
    {
      _type = type;
      _states = states is ValueState[] statesArray ? statesArray : states.ToArray();

      _type.AssertMatches(_states);
    }


    #region Equatable implementation
    // Return if the state equals another object
    public override bool Equals(object other)
    {
      return Equals(other as CompoundState);
    }

    // Return if the state equals another state
    public bool Equals(CompoundState other)
    {
      return other is not null
        && EqualityComparer<ExtensionType>.Default.Equals(_type, other._type)
        && EqualityComparer<State[]>.Default.Equals(_states, other._states);
    }

    // Return the hash code of the boolean
    public override int GetHashCode()
    {
      return HashCode.Combine(_type, _states);
    }


    // Return if the state equals another state using the equal operator
    public static bool operator ==(CompoundState left, CompoundState right)
    {
      return Equals(left, right);
    }
    
    // Return if the state does not equal another state using the not equal operator
    public static bool operator !=(CompoundState left, CompoundState right)
    {
      return !(left == right);
    }
    #endregion
  }
}