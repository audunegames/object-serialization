using System;

namespace Audune.Pickle
{
  // Class that defines an exception thrown when evaluating a path failed
  public class StatePathException : StateException
  {
    // Constructor
    public StatePathException(string message) : base(message)
    {
    }

    // Constructor with an inner exception
    public StatePathException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
