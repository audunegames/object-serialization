using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines the serializer for encoding and decoding states and objects
  public sealed class Serializer : ISerializationContext, IDeserializationContext, IExtensionTypeRegistry
  {
    // The encoder type of the pickler
    public readonly EncoderType encoderType;


    // The encoder for the pickler
    private readonly IEncoder _encoder;

    // The compound types for the pickler
    private readonly Dictionary<sbyte, ExtensionType> _compoundTypes = new();

    // The type adapters for the pickler
    private readonly Dictionary<Type, object> _typeAdapters = new() {
      { typeof(bool), TypeAdapter.Bool },
      { typeof(byte), TypeAdapter.UInt8 },
      { typeof(sbyte), TypeAdapter.Int8 },
      { typeof(ushort), TypeAdapter.UInt16 },
      { typeof(short), TypeAdapter.Int16 },
      { typeof(uint), TypeAdapter.UInt32 },
      { typeof(int), TypeAdapter.Int32 },
      { typeof(ulong), TypeAdapter.UInt64 },
      { typeof(long), TypeAdapter.Int64 },
      { typeof(float), TypeAdapter.Single },
      { typeof(double), TypeAdapter.Double },
      { typeof(string), TypeAdapter.String },
      { typeof(byte[]), TypeAdapter.Binary },
      { typeof(Vector2), TypeAdapter.Vector2 },
      { typeof(Vector3), TypeAdapter.Vector3 },
      { typeof(Vector4), TypeAdapter.Vector4 },
      { typeof(Vector2Int), TypeAdapter.Vector2Int },
      { typeof(Vector3Int), TypeAdapter.Vector3Int },
      { typeof(Color32), TypeAdapter.Color32 },
      { typeof(Color), TypeAdapter.Color },
      { typeof(Quaternion), TypeAdapter.Quaternion },
      { typeof(Rect), TypeAdapter.Rect },
      { typeof(RectInt), TypeAdapter.RectInt },
      { typeof(Bounds), TypeAdapter.Bounds },
      { typeof(BoundsInt), TypeAdapter.BoundsInt },
    };


    // Constructor
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

      foreach (var compoundType in ExtensionType.StandardExtensionTypes)
        RegisterExtensionType(compoundType);
    }


    #region Managing extension types
    // Register the specified extension type
    public void RegisterExtensionType(ExtensionType extensionType)
    {
      _compoundTypes.Add(extensionType.code, extensionType);
    }

    // Register all specified extension types
    public void RegisterExtensionTypes(IEnumerable<ExtensionType> extensionTypes)
    {
      foreach (var compoundType in extensionTypes)
        RegisterExtensionType(compoundType);
    }

    // Unregister the specified extension type
    public void UnregisterExtensionType(ExtensionType extensionType)
    {
      _compoundTypes.Remove(extensionType.code);
    }

    // Unregister all specified extension types
    public void UnregisterExtensionTypes(IEnumerable<ExtensionType> extensionTypes)
    {
      foreach (var extensionType in extensionTypes)
        UnregisterExtensionType(extensionType);
    }

    // Return if a compound type for the specified extension code exists and store the compound type
    bool IExtensionTypeRegistry.TryGetExtensionTypeByCode(sbyte extensionCode, out ExtensionType extensionType)
    {
      return _compoundTypes.TryGetValue(extensionCode, out extensionType);
    }
    #endregion

    #region Managing type adapters
    // Register a type adapter for the specified type
    public Serializer RegisterTypeAdapter<T>(ITypeAdapter<T> typeAdapter)
    {
      _typeAdapters.Add(typeof(T), typeAdapter);
      return this;
    }

    // Unregister a type adapter for the specified type
    public Serializer UnregisterTypeAdapter<T>()
    {
      _typeAdapters.Remove(typeof(T));
      return this;
    }

    // Return if a type adapter for the specified type exists and store the type adapter
    private bool TryGetTypeAdapter(Type type, out object typeAdapter)
    {
      typeAdapter = _typeAdapters.Where(e => e.Key.IsAssignableFrom(type)).Select(e => e.Value).FirstOrDefault();
      return typeAdapter != null;
    }
    #endregion

    #region Serializing objects
    // Serialize the specified object to a state
    public State Serialize<T>(T value)
    {
      if (value is ISerializable serializableValue)
        return serializableValue.Serialize(this);

      if (!TryGetTypeAdapter(typeof(T), out var typeAdapterObject))
        throw new SerializingException($"Unsupported object type {typeof(T)}");

      var typeAdapter = typeAdapterObject as ITypeAdapter<T>;
      return typeAdapter.ToState(value);
    }
    #endregion

    #region Deserializing objects
    // Deserialize the specified state to a new object
    public T Deserialize<T>(State state)
    {
      if (typeof(IDeserializable).IsAssignableFrom(typeof(T)))
      {
        try
        {
          var value = (T)Activator.CreateInstance(typeof(T));
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

      var typeAdapter = typeAdapterObject as ITypeAdapter<T>;
      return typeAdapter.FromState(state);
    }

    // Deserialize the specified state into an existing object
    public void Deserialize<T>(State state, T value)
    {
      if (value is IDeserializable deserializableValue)
      {
        deserializableValue.Deserialize(state, this);
        return;
      }
        
      if (!TryGetTypeAdapter(typeof(T), out var typeAdapterObject))
        throw new DeserializingException($"Unsupported object type {typeof(T)}");

      var typeAdapter = typeAdapterObject as ITypeAdapter<T>;
      typeAdapter.FromState(state, value);
    }
    #endregion

    #region Encoding states and objects
    // Encode the specified state to a byte array
    public byte[] EncodeState(State state)
    {
      return _encoder.Encode(state);
    }

    // Encode the specified serializable object to a byte array
    public byte[] Encode<T>(T value)
    {
      var state = Serialize(value);
      return EncodeState(state);
    }
    #endregion

    #region Decoding states and objects
    // Decode the specified byte array to a state
    public State DecodeState(byte[] data)
    {
      return _encoder.Decode(data);
    }

    // Decode the specified byte array to a new object
    public T Decode<T>(byte[] data)
    {
      var state = DecodeState(data);
      return Deserialize<T>(state);
    }

    // Decode the specified byte array into an existing object
    public void Decode<T>(byte[] data, T value)
    {
      var state = DecodeState(data);
      Deserialize<T>(state, value);
    }
    #endregion
  }
}