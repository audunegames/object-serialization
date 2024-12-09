using System;

namespace Audune.Serialization
{
  // Class that defines an exception thrown when the value of a state is of the wrong type
  public class StateValueException : StateException
  {
    // Constructor
    public StateValueException(Type expectedType, Type actualType) : base($"Expected value of type {expectedType}, but got {actualType}") 
    {
    }
  }
}
