namespace Audune.Pickle
{
  // Interface that defines a class as serializable
  public interface ISerializable
  {
    // Serialize the object to a state
    public State Serialize(ISerializationContext context);
  }
}