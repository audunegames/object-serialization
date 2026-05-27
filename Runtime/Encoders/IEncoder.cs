namespace Audune.Serialization
{
  // Interface that defines an encoder that encodes and decodes states
  public interface IEncoder
  {
    // Encode a state to a byte array
    public byte[] Encode(State state);

    // Decode a state from a byte array
    public State Decode(byte[] data);
  }
}