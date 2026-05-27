using System;
using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines a type adapter for types that convert to a <see cref="CompoundExtensionState"/>.
  /// </summary>
  /// <typeparam name="T">The type to serialize and deserialize.</typeparam>
  public interface ICompoundTypeAdapter<T> : ITypeAdapter<T>
  {    
    /// <summary>
    /// Return the extension type of the type adapter.
    /// </summary>
    public CompoundExtensionType extensionType { get; }


    /// <summary>
    /// Convert the specified value to a compound state.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A list of value states representing the converted value.</returns>
    /// <exception cref="StateException">If converting the state failed.</exception>
    public IReadOnlyList<ValueState> ToCompoundState(T value);

    /// <summary>
    /// Convert the specified compound state to a value.
    /// </summary>
    /// <param name="states">The list of value states to convert.</param>
    /// <returns>The value representing the converted list of value states.</returns>
    /// <exception cref="StateException">If converting the state failed.</exception>
    public T FromCompoundState(IReadOnlyList<ValueState> states);

    /// <summary>
    /// Convert the specified compound state into an existing value.
    /// </summary>
    /// <param name="states">The list of value states to convert.</param>
    /// <param name="value">The existing value to convert the list of value states into.</param>
    /// <exception cref="StateException">If converting the state failed.</exception>
    public void FromCompoundState(IReadOnlyList<ValueState> states, T value)
    {
      throw new StateException($"Type adapter {GetType()} does not support deserializing states into existing objects");
    }


    /// <inheritdoc/>
    State ITypeAdapter<T>.ToState(T value)
    {
      return new CompoundExtensionState(extensionType, ToCompoundState(value));
    }

    /// <inheritdoc/>
    T ITypeAdapter<T>.FromState(State state)
    {
      if (state is not CompoundExtensionState states)
        throw new InvalidOperationException($"Expected state of type {typeof(CompoundExtensionState)}, but got {state.GetType()}");

      if (states.type != extensionType)
        throw new InvalidOperationException($"Expected state with compound type {extensionType}, but got {states.type}");

      return FromCompoundState(states.states);
    }

    /// <inheritdoc/>
    void ITypeAdapter<T>.FromState(State state, T value)
    {
      if (state is not CompoundExtensionState states)
        throw new InvalidOperationException($"Expected state of type {typeof(CompoundExtensionState)}, but got {state.GetType()}");

      if (states.type != extensionType)
        throw new InvalidOperationException($"Expected state with compound type {extensionType}, but got {states.type}");

      FromCompoundState(states.states, value);
    }
  }
}