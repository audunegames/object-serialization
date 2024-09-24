using System;

namespace Audune.Pickle
{
  // Class that defines an exception thrown when encoding a state fails
  public class EncodingException : PickleException
  {
    // Constructor
    public EncodingException(string message) : base(message) { }
    public EncodingException(string message, Exception innerException) : base(message, innerException) { }
  }
}
