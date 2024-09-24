namespace Audune.Pickle
{
  // Interface that defines a type adapter for serializing and deserializing states
  public interface ITypeAdapter<T>
  {
    // Convert the specified value to a state
    public State ToState(T value);

    // Convert the specified state to a value
    public T FromState(State state);

    // Convert the specified state into an existing value
    public void FromState(State state, ref T value)
    {
      value = FromState(state);
    }
  }
}