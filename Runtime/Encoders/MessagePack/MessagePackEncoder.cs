using MessagePack;

namespace Audune.Serialization
{
  // Class that defines a MessagePack encoder
  public sealed class MessagePackEncoder : IEncoder
  {
    // The options of the encoder
    private MessagePackSerializerOptions _options;


    // Constructor
    public MessagePackEncoder(IExtensionTypeRegistry extensionTypeRegistry, MessagePackCompression compression)
    {
      _options = MessagePackSerializerOptions.Standard
        .WithResolver(new MessagePackStateFormatterResolver(extensionTypeRegistry))
        .WithCompression(compression);
    }
    

    #region Encoder implementation
    // Encode a state to a byte array
    public byte[] Encode(State state)
    {
      try
      {
        return MessagePackSerializer.Serialize(state, _options);
      }
      catch (MessagePackSerializationException ex)
      {
        throw new EncodingException(ex.Message, ex);
      }
    }

    // Decode a state from a byte array
    public State Decode(byte[] data)
    {
      try
      {
        return MessagePackSerializer.Deserialize<State>(data, _options);
      }
      catch (MessagePackSerializationException ex)
      {
        throw new DecodingException(ex.Message, ex);
      }
    }
    #endregion
  }
}