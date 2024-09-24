using System;

namespace Audune.Pickle
{
  // Class that defines an exception thrown when getting or setting a state failed
  public class StateException : PickleException
  {
    // Constructor
    public StateException(string message) : base(message) 
    {
    }

    // Constructor with an inner exception
    public StateException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
