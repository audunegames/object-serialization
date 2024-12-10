namespace Audune.Serialization
{
  // Interface that defines a class as deserializable
  public interface IDeserializable
  {
    // Deserialize the object from a state
    public void Deserialize(State state, IDeserializationContext context);
  }


  // Interface that defines a class as deserializable from the specified state type
  public interface IDeserializable<TState> : IDeserializable where TState : State
  {
    // Serialize the object to a state of the specified type
    public void Deserialize(TState state, IDeserializationContext context);


    // Serialize the object to a state
    void IDeserializable.Deserialize(State state, IDeserializationContext context)
    {
      if (state is not TState tState)
        throw new StateTypeException(typeof(TState), state.GetType());

      Deserialize(tState, context);
    }
  }
}