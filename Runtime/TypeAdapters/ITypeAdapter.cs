namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines a type adapter for serializing and deserializing states.
  /// </summary>
  /// <typeparam name="T">The type to serialize and deserialize.</typeparam>
  public interface ITypeAdapter<T>
  {
    /// <summary>
    /// Convert the specified value to a state.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A state representing the converted value.</returns>
    public State ToState(T value);

    /// <summary>
    /// Convert the specified state to a value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>A value representing the converted state.</returns>
    /// <exception cref="StateException">If converting the state failed.</exception>
    public T FromState(State state)
    {
      throw new StateException($"Type adapter {GetType()} does not support deserializing states to new objects");
    }

    /// <summary>
    /// Convert the specified state into an existing value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <param name="value">The existing value to convert the state into.</param>
    /// <exception cref="StateException">If converting the state failed.</exception>
    public void FromState(State state, T value)
    {
      throw new StateException($"Type adapter {GetType()} does not support deserializing states into existing objects");
    }
  }
}