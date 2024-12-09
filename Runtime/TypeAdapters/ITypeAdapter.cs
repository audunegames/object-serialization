namespace Audune.Serialization
{
  // Interface that defines a type adapter for serializing and deserializing states
  public interface ITypeAdapter<T>
  {
    // Convert the specified value to a state
    public State ToState(T value);

    // Convert the specified state to a value
    public T FromState(State state)
    {
      throw new StateException($"Type adapter {GetType()} does not support deserializing states to new objects");
    }

    // Convert the specified state into an existing value
    public void FromState(State state, T value)
    {
      throw new StateException($"Type adapter {GetType()} does not support deserializing states into existing objects");
    }
  }
}