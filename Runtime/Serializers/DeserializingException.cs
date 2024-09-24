using System;

namespace Audune.Pickle
{
  // Class that defines an exception thrown when deserializing an object fails
  public class DeserializingException : PickleException
  {
    // Constructor
    public DeserializingException(string message) : base(message) { }
    public DeserializingException(string message, Exception innerException) : base(message, innerException) { }
  }
}
