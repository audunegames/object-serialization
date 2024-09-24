using MessagePack;

namespace Audune.Pickle
{
  // Class that defines a MessagePack encoder
  public sealed class MessagePackEncoder : Encoder
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
    

    // Encode a state to a byte array
    public override byte[] Encode(State state)
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
    public override State Decode(byte[] data)
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
  }
}