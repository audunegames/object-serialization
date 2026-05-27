using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines the serializer for encoding and decoding states and objects.
  /// </summary>
  public sealed class Serializer : ISerializationContext, IDeserializationContext, IExtensionTypeRegistry
  {
    /// <summary>
    /// The encoder type of the serializer.
    /// </summary>
    public readonly EncoderType encoderType;


    /// <summary>
    /// The encoder for the serializer.
    /// </summary>
    private readonly IEncoder _encoder;

    /// <summary>
    /// The registered extension types for the serializer.
    /// </summary>
    private readonly Dictionary<sbyte, ExtensionType> _extensionTypes = new();

    /// <summary>
    /// The registered type adapters for the serializer.
    /// </summary>
    private readonly Dictionary<Type, object> _typeAdapters = new();


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="encoderType">The encoder type of the serializer.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="encoderType"/> is not a valid encoder type.</exception>
    public Serializer(EncoderType encoderType)
    {
      this.encoderType = encoderType;
      
      _encoder = encoderType switch
      {
        EncoderType.MessagePack => new MessagePackEncoder(this, MessagePackCompression.None),
        EncoderType.MessagePackLz4BlockArray => new MessagePackEncoder(this, MessagePackCompression.Lz4BlockArray),
        EncoderType.MessagePackLz4Block => new MessagePackEncoder(this, MessagePackCompression.Lz4Block),
        _ => throw new ArgumentOutOfRangeException($"Unsupported encoder typer {encoderType}"),
      };

      foreach (var extensionType in ExtensionType.StandardExtensionTypes)
        RegisterExtensionType(extensionType);
      
      foreach (var e in TypeAdapter.StandardTypeAdapters)
        _typeAdapters.Add(e.Key, e.Value);
    }


    #region Managing extension types
    /// <inheritdoc/>
    public void RegisterExtensionType(ExtensionType extensionType)
    {
      _extensionTypes.Add(extensionType.code, extensionType);
    }

    /// <inheritdoc/>
    public void RegisterExtensionTypes(IEnumerable<ExtensionType> extensionTypes)
    {
      foreach (var compoundType in extensionTypes)
        RegisterExtensionType(compoundType);
    }

    /// <inheritdoc/>
    public void UnregisterExtensionType(ExtensionType extensionType)
    {
      _extensionTypes.Remove(extensionType.code);
    }

    /// <inheritdoc/>
    public void UnregisterExtensionTypes(IEnumerable<ExtensionType> extensionTypes)
    {
      foreach (var extensionType in extensionTypes)
        UnregisterExtensionType(extensionType);
    }

    /// <inheritdoc/>
    bool IExtensionTypeRegistry.TryGetExtensionTypeByCode(sbyte extensionCode, out ExtensionType extensionType)
    {
      return _extensionTypes.TryGetValue(extensionCode, out extensionType);
    }
    #endregion

    #region Managing type adapters
    /// <summary>
    /// Register a type adapter for the specified type.
    /// </summary>
    /// <param name="typeAdapter">The type adapter to register.</param>
    /// <typeparam name="T">The type for which to register the type adapter.</typeparam>
    /// <returns>This serializer.</returns>
    public Serializer RegisterTypeAdapter<T>(ITypeAdapter<T> typeAdapter)
    {
      _typeAdapters.Add(typeof(T), typeAdapter);
      return this;
    }

    /// <summary>
    /// Unregister a type adapter for the specified type.
    /// </summary>
    /// <typeparam name="T">The type for which to unregister the type adapter.</typeparam>
    /// <returns>This serializer.</returns>
    public Serializer UnregisterTypeAdapter<T>()
    {
      _typeAdapters.Remove(typeof(T));
      return this;
    }

    /// <summary>
    /// Return if a type adapter for the specified type exists and store the type adapter in <paramref name="typeAdapter"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <param name="typeAdapter">The type adapter that matches the type.</param>
    /// <returns>If a type adapter for the specified type exists.</returns>
    private bool TryGetTypeAdapter(Type type, out object typeAdapter)
    {
      typeAdapter = _typeAdapters.Where(e => e.Key.IsAssignableFrom(type)).Select(e => e.Value).FirstOrDefault();
      return typeAdapter != null;
    }
    #endregion

    #region Serializing objects
    /// <summary>
    /// Serialize the specified object to a state.
    /// </summary>
    /// <param name="value">The value to deserialize.</param>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <returns>The serialized state.</returns>
    /// <exception cref="SerializingException">If the serialization failed.</exception>
    public State Serialize<T>(T value)
    {
      if (value is ISerializable serializableValue)
        return serializableValue.Serialize(this);

      if (!TryGetTypeAdapter(typeof(T), out var typeAdapterObject))
        throw new SerializingException($"Unsupported object type {typeof(T)}");

      if (typeAdapterObject is not ITypeAdapter<T> typeAdapter)
        throw new SerializingException($"Type adapter type mismatch for type {typeof(T)}");
      
      return typeAdapter.ToState(value);
    }
    #endregion

    #region Deserializing objects
    /// <summary>
    /// Deserialize the specified state to a new object.
    /// </summary>
    /// <param name="state">The state to deserialize.</param>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <returns>The deserialized object.</returns>
    /// <exception cref="DeserializingException">If the deserialization failed.</exception>
    public T Deserialize<T>(State state)
    {
      if (typeof(IDeserializable).IsAssignableFrom(typeof(T)))
      {
        try
        {
          var value = (T)Activator.CreateInstance(typeof(T), true);
          ((IDeserializable)value).Deserialize(state, this);
          return value;
        }
        catch (MissingMethodException ex)
        {
          throw new DeserializingException($"Deserializable type {typeof(T)} has no default constructor or is an abstract class", ex);
        }
      }

      if (!TryGetTypeAdapter(typeof(T), out var typeAdapterObject))
        throw new DeserializingException($"Unsupported object type {typeof(T)}");

      if (typeAdapterObject is not ITypeAdapter<T> typeAdapter)
        throw new DeserializingException($"Type adapter type mismatch for type {typeof(T)}");
      
      return typeAdapter.FromState(state);
    }

    /// <summary>
    /// Deserialize the specified state into an existing object.
    /// </summary>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="value">The existing object to deserialize the state into.</param>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <exception cref="DeserializingException">If the deserialization failed.</exception>
    public void Deserialize<T>(State state, T value)
    {
      if (value is IDeserializable deserializableValue)
      {
        deserializableValue.Deserialize(state, this);
        return;
      }
        
      if (!TryGetTypeAdapter(typeof(T), out var typeAdapterObject))
        throw new DeserializingException($"Unsupported object type {typeof(T)}");
      
      if (typeAdapterObject is not ITypeAdapter<T> typeAdapter)
        throw new DeserializingException($"Type adapter type mismatch for type {typeof(T)}");
      
      typeAdapter.FromState(state, value);
    }
    #endregion

    #region Encoding states and objects
    /// <summary>
    /// Encode the specified state to a byte array.
    /// </summary>
    /// <param name="state">The state to encode.</param>
    /// <returns>The encoded. byte array.</returns>
    public byte[] EncodeState(State state)
    {
      return _encoder.Encode(state);
    }

    /// <summary>
    /// Encode the specified serializable object to a byte array.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <typeparam name="T">The type of the value to encode.</typeparam>
    /// <returns>The encoded. byte array.</returns>
    public byte[] Encode<T>(T value)
    {
      var state = Serialize(value);
      return EncodeState(state);
    }
    #endregion

    #region Decoding states and objects
    /// <summary>
    /// Decode the specified byte array to a state.
    /// </summary>
    /// <param name="data">The byte array to decode.</param>
    /// <returns>The decoded state.</returns>
    public State DecodeState(byte[] data)
    {
      return _encoder.Decode(data);
    }

    /// <summary>
    /// Decode the specified byte array to a new object.
    /// </summary>
    /// <param name="data">The byte array to decode.</param>
    /// <typeparam name="T">The type of the object to decode.</typeparam>
    /// <returns>The decoded object.</returns>
    public T Decode<T>(byte[] data)
    {
      var state = DecodeState(data);
      return Deserialize<T>(state);
    }

    /// <summary>
    /// Decode the specified byte array into an existing object.
    /// </summary>
    /// <param name="data">The byte array to decode.</param>
    /// <param name="value">The existing object to decode the byte array into.</param>
    /// <typeparam name="T">The type of the object to decode.</typeparam>
    public void Decode<T>(byte[] data, T value)
    {
      var state = DecodeState(data);
      Deserialize(state, value);
    }
    #endregion
  }
}