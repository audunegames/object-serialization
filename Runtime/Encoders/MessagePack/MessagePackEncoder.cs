using MessagePack;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a MessagePack encoder.
  /// </summary>
  public sealed class MessagePackEncoder : IEncoder
  {
    /// <summary>
    /// The options of the encoder.
    /// </summary>
    private readonly MessagePackSerializerOptions _options;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="extensionTypeRegistry">The registry to use for encoding extension types.</param>
    /// <param name="compression">The compression to use.</param>
    public MessagePackEncoder(IExtensionTypeRegistry extensionTypeRegistry, MessagePackCompression compression)
    {
      _options = MessagePackSerializerOptions.Standard
        .WithResolver(new MessagePackStateFormatterResolver(extensionTypeRegistry))
        .WithCompression(compression);
    }
    

    #region Encoder implementation
    /// <summary>
    /// Encode a state to a byte array.
    /// </summary>
    /// <param name="state">The state to encode.</param>
    /// <returns>A byte array representing the encoded state.</returns>
    /// <exception cref="EncodingException">If the state could not be serialized to a MessagePack format.</exception>
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

    /// <summary>
    /// Decode a state from a byte array
    /// </summary>
    /// <param name="data">The byte array to decode.</param>
    /// <returns>A state representing the decoded byte array.</returns>
    /// <exception cref="DecodingException">If the state could not be deserialized from a MessagePack format.</exception>
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