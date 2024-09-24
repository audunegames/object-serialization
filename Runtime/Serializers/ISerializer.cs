namespace Audune.Pickle
{
  // Interface that defines a serializer
  public interface ISerializer<T>
  {
    // Serialize the specified object to a state
    public State Serialize(T value, ISerializationContext context);
  }
}