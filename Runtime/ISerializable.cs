namespace Audune.Pickle
{
  // Interface that defines a class as serializable
  public interface IPicklable<TState> where TState : State
  {
    // Serialize the object to a state
    public TState Serialize();

    // Deserialize the object from a state
    public void Deserialize(TState state);
  }


  // Interface that defines a class as serializable with the provided context
  public interface ISerializable<TState, TContext> where TState : State
  {
    // Serialize the object to a state
    public TState Serialize(TContext context);

    // Deserialize the object from a state
    public void Deserialize(TState state, TContext context);
  }
}