using System;

namespace Audune.Serialization
{
  // Class that defines an exception when running a serializer fails
  public class SerializerException : Exception
  {
    // Constructor
    public SerializerException(string message) : base(message)
    {
    }

    // Constructor with an inner exception
    public SerializerException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
