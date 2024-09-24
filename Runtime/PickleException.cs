using System;

namespace Audune.Pickle
{
  // Class that defines an exception when running a pickler fails
  public class PickleException : Exception
  {
    // Constructor
    public PickleException(string message) : base(message)
    {
    }

    // Constructor with an inner exception
    public PickleException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
