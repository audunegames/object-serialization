namespace Audune.Serialization
{
  // Interface that defines the context for a serializer
  public interface ISerializationContext
  {
    // Serialize the specified object to a state
    public State Serialize<T>(T value);
  }
}