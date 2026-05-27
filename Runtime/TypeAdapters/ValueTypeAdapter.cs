namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter types that convert to a <see cref="ValueState"/>.
  /// </summary>
  /// <typeparam name="T">The type to serialize and deserialize.</typeparam>
  public sealed class ValueTypeAdapter<T> : ITypeAdapter<T>
  {
    /// <inheritdoc/>
    State ITypeAdapter<T>.ToState(T value)
    {
      return new ValueState(value);
    }

    /// <inheritdoc/>
    T ITypeAdapter<T>.FromState(State state)
    {
      return state.AsValue().Get<T>();
    }
  }
}