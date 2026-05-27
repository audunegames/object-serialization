using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an exception thrown when a state is of the wrong type.
  /// </summary>
  public class StateTypeException : StateException
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="expectedType">The expected type of the state.</param>
    /// <param name="actualType">The actual type of the state.</param>
    public StateTypeException(Type expectedType, Type actualType) : base($"Expected state of type {expectedType}, but got {actualType}")
    {
    }
  }
}
