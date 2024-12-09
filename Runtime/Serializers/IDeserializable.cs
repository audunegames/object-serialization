namespace Audune.Serialization
{
  // Interface that defines a class as deserializable
  public interface IDeserializable
  {
    // Deserialize the object from a state
    public void Deserialize(State state, IDeserializationContext context);
  }
}