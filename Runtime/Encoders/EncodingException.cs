using System;

namespace Audune.Serialization
{
  // Class that defines an exception thrown when encoding a state fails
  public class EncodingException : SerializerException
  {
    // Constructor
    public EncodingException(string message) : base(message) { }
    public EncodingException(string message, Exception innerException) : base(message, innerException) { }
  }
}
