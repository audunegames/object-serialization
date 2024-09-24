using System;

namespace Audune.Pickle
{
  // Class that defines an exception thrown when decoding a state fails
  public class DecodingException : PickleException
  {
    // Constructor
    public DecodingException(string message) : base(message) { }
    public DecodingException(string message, Exception innerException) : base(message, innerException) { }
  }
}
