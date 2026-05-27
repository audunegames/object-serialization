using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an exception thrown when encoding a state fails.
  /// </summary>
  public class EncodingException : SerializerException
  {
    /// <summary>
    /// Constructor for a message.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    public EncodingException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor for a message and inner exception.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    /// <param name="innerException">The exception that caused this exception to be thrown.</param>
    public EncodingException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
