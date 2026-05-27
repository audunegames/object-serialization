namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for types that convert to a <see cref="RawExtensionState"/> based on provided delegates.
  /// </summary>
  /// <typeparam name="T">The type to serialize and deserialize.</typeparam>
  public sealed class SimpleRawTypeAdapter<T> : IRawTypeAdapter<T>
  {
    /// <summary>
    /// Delegate the defines a serializer that converts a value into a byte array.
    /// </summary>
    public delegate byte[] Serializer(T value);
    
    /// <summary>
    /// Delegate that defines a deserialized that converts a byte array. into a value.
    /// </summary>
    public delegate T Deserializer(byte[] bytes);


    /// <summary>
    /// The extension type of the type adapter.
    /// </summary>
    private readonly RawExtensionType _extensionType;

    /// <summary>
    /// The serializer of the type adapter.
    /// </summary>
    private readonly Serializer _serializer;
    
    /// <summary>
    /// The deserializer of the type adapter.
    /// </summary>
    private readonly Deserializer _deserializer;


    /// <inheritdoc/>
    RawExtensionType IRawTypeAdapter<T>.extensionType => _extensionType;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="extensionType">The extension type of the type adapter.</param>
    /// <param name="serializer">The serializer of the type adapter.</param>
    /// <param name="deserializer">The deserializer of the type adapter.</param>
    public SimpleRawTypeAdapter(RawExtensionType extensionType, Serializer serializer, Deserializer deserializer)
    {
      _extensionType = extensionType;
      _serializer = serializer;
      _deserializer = deserializer;
    }


    /// <inheritdoc/>
    byte[] IRawTypeAdapter<T>.ToBytes(T value)
    {
      return _serializer(value);
    }

    /// <inheritdoc/>
    T IRawTypeAdapter<T>.FromBytes(byte[] bytes)
    {
      return _deserializer(bytes);
    }
  }
}