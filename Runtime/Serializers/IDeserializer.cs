namespace Audune.Pickle
{
  // Interface that defines a deserializer
  public interface IDeserializer<T>
  {
    // Deserialize the specified state to a new object
    public T Deserialize(State state, IDeserializationContext context);

    // Deserialize the specified state into an existing object
    public void DeserializeInto(State state, T value, IDeserializationContext context);
  }
}