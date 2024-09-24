using System;
using System.Collections.Generic;

namespace Audune.Pickle
{
  // Class that defines a simple compound type adapter
  public sealed class SimpleCompoundTypeAdapter<T> : ICompoundTypeAdapter<T>
  {
    // Delegates for serializers and deserializers of the type adapter
    public delegate IReadOnlyList<ValueState> Serializer(T value);
    public delegate T Deserializer(IReadOnlyList<ValueState> states);


    // The extension type of the type adapter
    private CompoundExtensionType _extensionType;

    // The serializer and deserializer of the type adapter
    private Serializer _serializer;
    private Deserializer _deserializer;


    // Return the extension type of the type adapter
    CompoundExtensionType ICompoundTypeAdapter<T>.extensionType => _extensionType;


    // Constructor
    public SimpleCompoundTypeAdapter(CompoundExtensionType extensionType, Serializer serializer, Deserializer deserializer)
    {
      _extensionType = extensionType;
      _serializer = serializer;
      _deserializer = deserializer;
    }


    // Convert the specified value to a compound state
    IReadOnlyList<ValueState> ICompoundTypeAdapter<T>.ToCompoundState(T value)
    {
      return _serializer(value);
    }

    // Convert the specified compound state to a value
    T ICompoundTypeAdapter<T>.FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return _deserializer(states);
    }
  }
}