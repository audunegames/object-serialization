namespace Audune.Serialization
{
  // Interface that defines the context for a deserializer
  public interface IDeserializationContext
  {
    // Deserialize the specified state to a new object
    public T Deserialize<T>(State state);

    // Deserialize the specified state into an existing object
    public void Deserialize<T>(State state, T value)
    {
      value = Deserialize<T>(state);
    }
  }
}