namespace Audune.Serialization
{
  // Class that defines a type adapter for value types
  public sealed class ValueTypeAdapter<T> : ITypeAdapter<T>
  {
    // Convert the specified value to a state
    State ITypeAdapter<T>.ToState(T value)
    {
      return new ValueState(value);
    }

    // Convert the specified state to a value
    T ITypeAdapter<T>.FromState(State state)
    {
      return state.AsValue().Get<T>();
    }
  }
}