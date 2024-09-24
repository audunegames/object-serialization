using System;

namespace Audune.Pickle
{
  // Class that defines an exception thrown when serializing an object fails
  public class SerializingException : PickleException
  {
    // Constructor
    public SerializingException(string message) : base(message) { }
    public SerializingException(string message, Exception innerException) : base(message, innerException) { }
  }
}
