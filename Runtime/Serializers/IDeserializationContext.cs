namespace Audune.Serialization
{
  /// <summary>
  /// // Interface that defines the context for deserialization.
  /// </summary>
  public interface IDeserializationContext
  {
    /// <summary>
    /// Deserialize the specified state to a new object.
    /// </summary>
    /// <param name="state">The state to deserialize.</param>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <returns>The deserialized object.</returns>
    public T Deserialize<T>(State state);

    /// <summary>
    /// Deserialize the specified state into an existing object.
    /// </summary>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="value">The existing object to deserialize the state into.</param>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    public void Deserialize<T>(State state, T value);
  }
}