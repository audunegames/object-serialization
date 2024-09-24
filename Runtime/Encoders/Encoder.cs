namespace Audune.Pickle
{
  // Base class that defines an encoder that encodes and decodes states
  public abstract class Encoder
  {
    // Encode a state to a byte array
    public abstract byte[] Encode(State state);

    // Decode a state from a byte array
    public abstract State Decode(byte[] data);
  }
}