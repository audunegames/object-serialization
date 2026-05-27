namespace Audune.Serialization
{
  /// <summary>
  /// Interface that marks a class as both serializable and deserializable.
  /// </summary>
  public interface IPersistable : ISerializable, IDeserializable
  {
  }


  /// <summary>
  /// Interface that defines a class as both serializable to and deserializable from the specified state type
  /// </summary>
  /// <typeparam name="TState">The type of the state to serialize to and deserialize from.</typeparam>
  public interface IPersistable<TState> : IPersistable, ISerializable<TState>, IDeserializable<TState> where TState : State
  {
  }
}