using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for types that convert to a <see cref="CompoundExtensionState"/> based on provided delegates.
  /// </summary>
  /// <typeparam name="T">The type to serialize and deserialize.</typeparam>
  public sealed class SimpleCompoundTypeAdapter<T> : ICompoundTypeAdapter<T>
  {
    /// <summary>
    /// Delegate the defines a serializer that converts a value into a list of value states.
    /// </summary>
    public delegate IReadOnlyList<ValueState> Serializer(T value);
    
    /// <summary>
    /// Delegate that defines a deserialized that converts a list of value states into a value.
    /// </summary>
    public delegate T Deserializer(IReadOnlyList<ValueState> states);


    /// <summary>
    /// The extension type of the type adapter.
    /// </summary>
    private readonly CompoundExtensionType _extensionType;

    /// <summary>
    /// The serializer of the type adapter.
    /// </summary>
    private readonly Serializer _serializer;
    
    /// <summary>
    /// The deserializer of the type adapter.
    /// </summary>
    private readonly Deserializer _deserializer;


    /// <inheritdoc/>
    CompoundExtensionType ICompoundTypeAdapter<T>.extensionType => _extensionType;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="extensionType">The extension type of the type adapter.</param>
    /// <param name="serializer">The serializer of the type adapter.</param>
    /// <param name="deserializer">The deserializer of the type adapter.</param>
    public SimpleCompoundTypeAdapter(CompoundExtensionType extensionType, Serializer serializer, Deserializer deserializer)
    {
      _extensionType = extensionType;
      _serializer = serializer;
      _deserializer = deserializer;
    }


    /// <inheritdoc/>
    IReadOnlyList<ValueState> ICompoundTypeAdapter<T>.ToCompoundState(T value)
    {
      return _serializer(value);
    }

    /// <inheritdoc/>
    T ICompoundTypeAdapter<T>.FromCompoundState(IReadOnlyList<ValueState> states)
    {
      return _deserializer(states);
    }
  }
}