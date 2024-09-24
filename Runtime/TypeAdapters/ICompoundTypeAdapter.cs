using System;
using System.Collections.Generic;

namespace Audune.Pickle
{
  // Interface that defines a type adapter for compound extension types
  public interface ICompoundTypeAdapter<T> : ITypeAdapter<T>
  {    
    // The extension type of the type adapter
    public CompoundExtensionType extensionType { get; }


    // Convert the specified value to a compound state
    public IReadOnlyList<ValueState> ToCompoundState(T value);

    // Convert the specified compound state to a value
    public T FromCompoundState(IReadOnlyList<ValueState> states);

    // Convert the specified compound state into an existing value
    public void FromCompoundState(IReadOnlyList<ValueState> states, ref T value)
    {
      value = FromCompoundState(states);
    }


    // Convert the specified value to a state
    State ITypeAdapter<T>.ToState(T value)
    {
      return new CompoundState(extensionType, ToCompoundState(value));
    }

    // Convert the specified state to a value
    T ITypeAdapter<T>.FromState(State state)
    {
      if (state is not CompoundState states)
        throw new InvalidOperationException($"Expected state of type {typeof(CompoundState)}, but got {state.GetType()}");

      if (states.type != extensionType)
        throw new InvalidOperationException($"Expected state with compound type {extensionType}, but got {states.type}");

      return FromCompoundState(states.states);
    }

    // Convert the specified state into an existing value
    void ITypeAdapter<T>.FromState(State state, ref T value)
    {
      if (state is not CompoundState states)
        throw new InvalidOperationException($"Expected state of type {typeof(CompoundState)}, but got {state.GetType()}");

      if (states.type != extensionType)
        throw new InvalidOperationException($"Expected state with compound type {extensionType}, but got {states.type}");

      FromCompoundState(states.states, ref value);
    }
  }
}