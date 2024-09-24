namespace Audune.Pickle
{
  // Class that defines a simple raw type adapter
  public sealed class SimpleRawTypeAdapter<T> : IRawTypeAdapter<T>
  {
    // Delegates for serializers and deserializers of the type adapter
    public delegate byte[] Serializer(T value);
    public delegate T Deserializer(byte[] bytes);


    // The extension type of the type adapter
    private RawExtensionType _extensionType;

    // The serializer and deserializer of the type adapter
    private Serializer _serializer;
    private Deserializer _deserializer;


    // Return the extension type of the type adapter
    RawExtensionType IRawTypeAdapter<T>.extensionType => _extensionType;


    // Constructor
    public SimpleRawTypeAdapter(RawExtensionType extensionType, Serializer serializer, Deserializer deserializer)
    {
      _extensionType = extensionType;
      _serializer = serializer;
      _deserializer = deserializer;
    }


    // Convert the specified value to a byte array
    byte[] IRawTypeAdapter<T>.ToBytes(T value)
    {
      return _serializer(value);
    }

    // Convert the specified byte array to a value
    T IRawTypeAdapter<T>.FromBytes(byte[] bytes)
    {
      return _deserializer(bytes);
    }
  }
}