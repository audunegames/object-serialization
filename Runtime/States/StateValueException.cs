using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an exception thrown when the value of a state is of the wrong type.
  /// </summary>
  public class StateValueException : StateException
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="expectedType">The expected type of the value of the state.</param>
    /// <param name="actualType">The actual type of the value of the state.</param>
    public StateValueException(Type expectedType, Type actualType) : base($"Expected value of type {expectedType}, but got {actualType}") 
    {
    }
  }
}
