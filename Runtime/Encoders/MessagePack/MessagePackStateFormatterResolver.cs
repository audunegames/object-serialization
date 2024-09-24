using MessagePack;
using MessagePack.Formatters;

namespace Audune.Pickle
{
  // Class that defines a MessagePack formatter resolver for states
  internal class MessagePackStateFormatterResolver : IFormatterResolver
  {
    // The compound type registry for the resolver
    private readonly IExtensionTypeRegistry _extensionTypeRegistry;

    // The formatters for the resolver
    private readonly IMessagePackFormatter<State> _stateFormatter;

    // Constructor
    public MessagePackStateFormatterResolver(IExtensionTypeRegistry extensionTypeRegistry)
    {
      _extensionTypeRegistry = extensionTypeRegistry;

      _stateFormatter = new MessagePackStateFormatter(_extensionTypeRegistry);
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
