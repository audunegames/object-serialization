using System;

namespace Audune.Serialization
{
  // Class that defines an exception thrown when decoding a state fails
  public class DecodingException : SerializerException
  {
    // Constructor
    public DecodingException(string message) : base(message) { }
    public DecodingException(string message, Exception innerException) : base(message, innerException) { }
  }
}
