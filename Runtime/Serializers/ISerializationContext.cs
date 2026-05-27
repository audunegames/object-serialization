namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines the context for serialization.
  /// </summary>
  public interface ISerializationContext
  {
    /// <summary>
    /// Serialize the specified object to a state.
    /// </summary>
    /// <param name="value">The value to deserialize.</param>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <returns>The serialized state.</returns>
    public State Serialize<T>(T value);
  }
}