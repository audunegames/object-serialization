using System;

namespace Audune.Serialization
{
  // Class that defines an exception thrown when a state is of the wrong type
  public class StateTypeException : StateException
  {
    // Constructor
    public StateTypeException(Type expectedType, Type actualType) : base($"Expected state of type {expectedType}, but got {actualType}")
    {
    }
  }
}
