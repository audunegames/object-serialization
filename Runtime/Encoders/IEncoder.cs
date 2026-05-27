namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines an encoder that encodes and decodes states.
  /// </summary>
  public interface IEncoder
  {
    /// <summary>
    /// Encode a state to a byte array.
    /// </summary>
    /// <param name="state">The state to encode.</param>
    /// <returns>A byte array representing the encoded state.</returns>
    public byte[] Encode(State state);

    /// <summary>
    /// Decode a state from a byte array
    /// </summary>
    /// <param name="data">The byte array to decode.</param>
    /// <returns>A state representing the decoded byte array.</returns>
    /// <exception cref="DecodingException">If the state could not be deserialized from a MessagePack format.</exception>
    public State Decode(byte[] data);
  }
}