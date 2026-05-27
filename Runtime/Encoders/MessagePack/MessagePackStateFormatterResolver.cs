using MessagePack;
using MessagePack.Formatters;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a MessagePack formatter resolver for states.
  /// </summary>
  internal class MessagePackStateFormatterResolver : IFormatterResolver
  {
    /// <summary>
    /// The formatters for the resolver.
    /// </summary>
    private readonly IMessagePackFormatter<State> _stateFormatter;

    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="extensionTypeRegistry">The registry to use for encoding extension types.</param>
    public MessagePackStateFormatterResolver(IExtensionTypeRegistry extensionTypeRegistry)
    {
      _stateFormatter = new MessagePackStateFormatter(extensionTypeRegistry);
    }


    /// <summary>
    /// Return the formatter for the specified type.
    /// </summary>
    /// <typeparam name="T">The type to return the formatter for.</typeparam>
    /// <returns>The formatter corresponding to the type.</returns>
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      if (typeof(State).IsAssignableFrom(typeof(T)))
        return (IMessagePackFormatter<T>)_stateFormatter;
      else
        return null;
    }
  }
}
