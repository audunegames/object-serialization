namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines a type adapter for types that convert to a <see cref="RawExtensionState"/>.
  /// </summary>
  /// <typeparam name="T">The type to serialize and deserialize.</typeparam>
  public interface IRawTypeAdapter<T> : ITypeAdapter<T>
  {    
    /// <summary>
    /// Return the extension type of the type adapter
    /// </summary>
    public RawExtensionType extensionType { get; }


    /// <summary>
    /// Convert the specified value to a byte array.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A byte array representing the converted value.</returns>
    /// <exception cref="StateException">If converting the state failed.</exception>
    public byte[] ToBytes(T value);

    /// <summary>
    /// Convert the specified byte array to a value.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <returns>A value that represents the converted byte array.</returns>
    /// <exception cref="StateException">If converting the state failed.</exception>
    public T FromBytes(byte[] bytes);

    /// <summary>
    /// Convert the specified byte array into an existing value.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <param name="value">The existing value to convert the byte array into.</param>
    /// <exception cref="StateException">If converting the state failed.</exception>
    public void FromBytes(byte[] bytes, T value)
    {
      throw new StateException($"Type adapter {GetType()} does not support deserializing states into existing objects");
    }


    /// <inheritdoc/>
    State ITypeAdapter<T>.ToState(T value)
    {
      return new RawExtensionState(extensionType, ToBytes(value));
    }

    /// <inheritdoc/>
    T ITypeAdapter<T>.FromState(State state)
    {
      if (state is not RawExtensionState rawState)
        throw new StateTypeException(typeof(RawExtensionState), state.GetType());

      if (rawState.type != extensionType)
        throw new StateException($"Expected state with extension type {extensionType}, but got {rawState.type}");

      return FromBytes(rawState.bytes);
    }

    /// <inheritdoc/>
    void ITypeAdapter<T>.FromState(State state, T value)
    {
      throw new StateException($"Type adapter {GetType()} does not support deserializing states into existing objects");
    }
  }
}