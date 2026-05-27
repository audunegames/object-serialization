using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an exception when running a serializer fails.
  /// </summary>
  public class SerializerException : Exception
  {
    /// <summary>
    /// Constructor for a message.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    public SerializerException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor for a message and inner exception.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    /// <param name="innerException">The exception that caused this exception to be thrown.</param>
    public SerializerException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
