using System;

namespace Audune.Serialization
{
  // Class that defines an exception thrown when deserializing an object fails
  public class DeserializingException : SerializerException
  {
    // Constructor
    public DeserializingException(string message) : base(message) { }
    public DeserializingException(string message, Exception innerException) : base(message, innerException) { }
  }
}
