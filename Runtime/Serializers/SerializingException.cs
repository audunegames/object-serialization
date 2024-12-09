using System;

namespace Audune.Serialization
{
  // Class that defines an exception thrown when serializing an object fails
  public class SerializingException : SerializerException
  {
    // Constructor
    public SerializingException(string message) : base(message) { }
    public SerializingException(string message, Exception innerException) : base(message, innerException) { }
  }
}
