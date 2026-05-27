using MessagePack;
using MessagePack.Formatters;

namespace Audune.Serialization
{
  // Class that defines a MessagePack formatter resolver for states
  internal class MessagePackStateFormatterResolver : IFormatterResolver
  {
    // The formatters for the resolver
    private readonly IMessagePackFormatter<State> _stateFormatter;

    // Constructor
    public MessagePackStateFormatterResolver(IExtensionTypeRegistry extensionTypeRegistry)
    {
      _stateFormatter = new MessagePackStateFormatter(extensionTypeRegistry);
    }


    // Return the formatter for the specified type
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      if (typeof(State).IsAssignableFrom(typeof(T)))
        return (IMessagePackFormatter<T>)_stateFormatter;
      else
        return null;
    }
  }
}
