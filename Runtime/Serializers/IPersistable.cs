namespace Audune.Serialization
{
  // Interface that defines a class as both serializable and deserializable
  public interface IPersistable : ISerializable, IDeserializable
  {
  }


  // Interface that defines a class as both serializable to and deserializable from the specified state type
  public interface IPersistable<TState> : IPersistable, ISerializable<TState>, IDeserializable<TState> where TState : State
  {
  }
}