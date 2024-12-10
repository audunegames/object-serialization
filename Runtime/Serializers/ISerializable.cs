namespace Audune.Serialization
{
  // Interface that defines a class as serializable
  public interface ISerializable
  {
    // Serialize the object to a state
    public State Serialize(ISerializationContext context);
  }


  // Interface that defines a class as serializable to the specified state type
  public interface ISerializable<TState> : ISerializable where TState : State
  {
    // Serialize the object to a state of the specified type
    public new TState Serialize(ISerializationContext context);


    // Serialize the object to a state
    State ISerializable.Serialize(ISerializationContext context)
    {
      return Serialize(context);
    }
  }
}