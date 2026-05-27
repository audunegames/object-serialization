namespace Audune.Serialization
{
  /// <summary>
  /// Interface that marks a class as serializable/
  /// </summary>
  public interface ISerializable
  {
    /// <summary>
    /// Serialize the object to a state.
    /// </summary>
    /// <param name="context">The context for serialization.</param>
    /// <returns>The serialized state.</returns>
    public State Serialize(ISerializationContext context);
  }


  /// <summary>
  /// Interface that marks a class as serializable to the specified state type.
  /// </summary>
  /// <typeparam name="TState">The type of the state to serialize to.</typeparam>
  public interface ISerializable<TState> : ISerializable where TState : State
  {
    /// <summary>
    /// Serialize the object to a state of the specified type.
    /// </summary>
    /// <param name="context">The context for serialization.</param>
    /// <returns>The serialized state.</returns>
    public new TState Serialize(ISerializationContext context);


    /// <summary>
    /// Serialize the object to a state.
    /// </summary>
    /// <param name="context">The context for serialization.</param>
    /// <returns>The serialized state.</returns>
    State ISerializable.Serialize(ISerializationContext context)
    {
      return Serialize(context);
    }
  }
}