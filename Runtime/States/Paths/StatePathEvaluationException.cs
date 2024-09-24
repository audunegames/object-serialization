using System;

namespace Audune.Pickle
{
  // Class that defines an exception thrown while evaluating paths
  public class StatePathEvaluationException : Exception
  {
    // Constructor
    public StatePathEvaluationException(string message) : base(message) { }
    public StatePathEvaluationException(string message, Exception innerException) : base(message, innerException) { }
  }
}
