namespace Audune.Serialization
{
  /// <summary>
  /// Interface that marks a class as deserializable.
  /// </summary>
  public interface IDeserializable
  {
    /// <summary>
    /// Deserialize the object from a state.
    /// </summary>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="context">The context for deserialization.</param>
    public void Deserialize(State state, IDeserializationContext context);
  }


  /// <summary>
  /// Interface that marks a class as deserializable from the specified state type.
  /// </summary>
  /// <typeparam name="TState">The type of the state to deserialize from.</typeparam>
  public interface IDeserializable<TState> : IDeserializable where TState : State
  {
    /// <summary>
    /// Deserialize the object from a state of the specified type.
    /// </summary>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="context">The context for deserialization.</param>
    public void Deserialize(TState state, IDeserializationContext context);


    /// <summary>
    /// Deserialize the object from a state.
    /// </summary>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="context">The context for deserialization.</param>
    /// <exception cref="StateTypeException">If the state is of the wrong type.</exception>
    void IDeserializable.Deserialize(State state, IDeserializationContext context)
    {
      if (state is not TState tState)
        throw new StateTypeException(typeof(TState), state.GetType());

      Deserialize(tState, context);
    }
  }
}