namespace Audune.Serialization
{
  /// <summary>
  /// Enum that defines the type of an encoder
  /// </summary>
  public enum EncoderType
  {
    /// <summary>
    /// Use MessagePack for encoding the states.
    /// </summary>
    MessagePack,
    
    /// <summary>
    /// Use MessagePack with LZ4 block compression for encoding the states.
    /// </summary>
    MessagePackLz4Block,
    
    /// <summary>
    /// Use MessagePack with LZ4 block array compression for encoding the states.
    /// </summary>
    MessagePackLz4BlockArray,
  }
}