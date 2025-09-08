using MessagePack;
using MessagePack.Formatters;
using System.Buffers;
using System.Collections.Generic;

namespace Audune.Serialization
{
  // Class that defines a MessagePack formatter for states
  internal class MessagePackStateFormatter : IMessagePackFormatter<State>
  {
    // The compound type registry for the formatter
    private readonly IExtensionTypeRegistry _extensionTypeRegistry;

    // Constructor
    public MessagePackStateFormatter(IExtensionTypeRegistry compoundTypeRegistry)
    {
      _extensionTypeRegistry = compoundTypeRegistry;
    }


    #region Serialization methods
    // Serialize a state
    public void Serialize(ref MessagePackWriter writer, State state, MessagePackSerializerOptions options)
    {
      switch (state)
      {
        case ValueState valueState:
          SerializeValueState(ref writer, valueState);
          break;

        case ListState listState:
          SerializeListState(ref writer, listState, options);
          break;

        case ObjectState objectState:
          SerializeObjectState(ref writer, objectState, options);
          break;

        case CompoundExtensionState compoundState:
          SerializeCompoundState(ref writer, compoundState, options);
          break;

        case RawExtensionState rawState:
          SerializeRawState(ref writer, rawState, options);
          break;

        default:
          throw new MessagePackSerializationException($"Unsupported state type {state.GetType()}");
      }
    }

    // Serialize a value state
    private void SerializeValueState(ref MessagePackWriter writer, ValueState state)
    {
      if (state == null || state.value is null)
        writer.WriteNil();
      else if (state.value is bool boolValue)
        writer.Write(boolValue);
      else if (state.value is byte byteValue)
        writer.Write(byteValue);
      else if (state.value is sbyte sbyteValue)
        writer.Write(sbyteValue);
      else if (state.value is ushort ushortValue)
        writer.Write(ushortValue);
      else if (state.value is short shortValue)
        writer.Write(shortValue);
      else if (state.value is uint uintValue)
        writer.Write(uintValue);
      else if (state.value is int intValue)
        writer.Write(intValue);
      else if (state.value is ulong ulongValue)
        writer.Write(ulongValue);
      else if (state.value is long longValue)
        writer.Write(longValue);
      else if (state.value is float floatValue)
        writer.Write(floatValue);
      else if (state.value is double doubleValue)
        writer.Write(doubleValue);
      else if (state.value is string stringValue)
        writer.Write(stringValue);
      else if (state.value is byte[] byteArrayValue)
        writer.Write(byteArrayValue);
      else
        throw new MessagePackSerializationException($"Unsupported value state value {state.value.GetType()}");
    }

    // Serialize a list state
    private void SerializeListState(ref MessagePackWriter writer, ListState state, MessagePackSerializerOptions options)
    {
      writer.WriteArrayHeader(state.count);
      foreach (var item in state)
      {
        Serialize(ref writer, item, options);
      }
    }

    // Serialize an object state
    private void SerializeObjectState(ref MessagePackWriter writer, ObjectState state, MessagePackSerializerOptions options)
    {
      writer.WriteMapHeader(state.count);
      foreach (var field in state)
      {
        writer.Write(field.Key);
        Serialize(ref writer, field.Value, options);
      }
    }

    // Serialize a compound state
    private void SerializeCompoundState(ref MessagePackWriter writer, CompoundExtensionState state, MessagePackSerializerOptions options)
    {
      var extensionBuffer = new ArrayBufferWriter<byte>();
      var extensionWriter = writer.Clone(extensionBuffer);
      foreach (var childState in state.states)
        Serialize(ref extensionWriter, childState, options);
      extensionWriter.Flush();

      writer.WriteExtensionFormat(new ExtensionResult(state.type.code, new ReadOnlySequence<byte>(extensionBuffer.WrittenMemory)));
    }

    // Serialize a raw state
    private void SerializeRawState(ref MessagePackWriter writer, RawExtensionState state, MessagePackSerializerOptions options)
    {
      var extensionBuffer = new ArrayBufferWriter<byte>();
      var extensionWriter = writer.Clone(extensionBuffer);
      extensionWriter.WriteRaw(state.bytes);
      extensionWriter.Flush();

      writer.WriteExtensionFormat(new ExtensionResult(state.type.code, new ReadOnlySequence<byte>(extensionBuffer.WrittenMemory)));
    }
    #endregion

    #region Deserialization methods
    // Deserialize a state
    public State Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      options.Security.DepthStep(ref reader);

      var state = reader.NextMessagePackType switch
      {
        MessagePackType.Nil => new ValueState(null),
        MessagePackType.Boolean => new ValueState(reader.ReadBoolean()),
        MessagePackType.Integer => DeserializeIntegerFormat(ref reader),
        MessagePackType.Float => DeserializeFloatFormat(ref reader),
        MessagePackType.String => new ValueState(reader.ReadString()),
        MessagePackType.Binary => DeserializeBinaryFormat(ref reader),
        MessagePackType.Array => DeserializeArrayFormat(ref reader, options),
        MessagePackType.Map => DeserializeMapFormat(ref reader, options),
        MessagePackType.Extension => DeserializeExtensionFormat(ref reader, options),
        _ => throw new MessagePackSerializationException($"Unsupported format {MessagePackCode.ToFormatName(reader.NextCode)}"),
      };
      
      reader.Depth--;
      return state;
    }

    // Deserialize an integer format
    private State DeserializeIntegerFormat(ref MessagePackReader reader)
    {
      return reader.NextCode switch
      {
        MessagePackCode.UInt8 => new ValueState(reader.ReadByte()),
        MessagePackCode.Int8 => new ValueState(reader.ReadSByte()),
        MessagePackCode.UInt16 =>  new ValueState(reader.ReadUInt16()),
        MessagePackCode.Int16 =>  new ValueState(reader.ReadInt16()),
        MessagePackCode.UInt32 =>  new ValueState(reader.ReadUInt32()),
        MessagePackCode.Int32 =>  new ValueState(reader.ReadInt32()),
        MessagePackCode.UInt64 =>  new ValueState(reader.ReadUInt64()),
        MessagePackCode.Int64 =>  new ValueState(reader.ReadInt64()),
        >= MessagePackCode.MinFixInt and <= MessagePackCode.MaxFixInt => new ValueState(reader.ReadByte()),
        >= MessagePackCode.MinNegativeFixInt and <= MessagePackCode.MaxNegativeFixInt => new ValueState(reader.ReadSByte()),
        _ => throw new MessagePackSerializationException($"Unsupported int format {MessagePackCode.ToFormatName(reader.NextCode)}"),
      };
    }

    // Deserialize a float format
    private State DeserializeFloatFormat(ref MessagePackReader reader)
    {
      return reader.NextCode switch
      {
        MessagePackCode.Float32 => new ValueState(reader.ReadSingle()),
        MessagePackCode.Float64 => new ValueState(reader.ReadDouble()),
        _ => throw new MessagePackSerializationException($"Unsupported float format {MessagePackCode.ToFormatName(reader.NextCode)}"),
      };
    }

    // Deserialize a binary format
    private State DeserializeBinaryFormat(ref MessagePackReader reader)
    {
      var byteSpan = reader.ReadBytes();
      return new ValueState(byteSpan.HasValue ? byteSpan.Value.ToArray() : new byte[0]);
    }

    // Deserialize an array format
    private ListState DeserializeArrayFormat(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      var length = reader.ReadArrayHeader();
      var listState = new ListState();
      for (int i = 0; i < length; i++)
      {
        var state = Deserialize(ref reader, options);
        listState.Add(state);
      }
      return listState;
    }

    // Deserialize a map format
    private ObjectState DeserializeMapFormat(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      var length = reader.ReadMapHeader();
      var objectState = new ObjectState();
      for (int i = 0; i < length; i++)
      {
        var name = reader.ReadString();
        var state = Deserialize(ref reader, options);
        objectState.Set(name, state);
      }
      return objectState;
    }

    // Deserialize an extension format
    private State DeserializeExtensionFormat(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      var header = reader.ReadExtensionFormatHeader();

      if (!_extensionTypeRegistry.TryGetExtensionTypeByCode(header.TypeCode, out var type))
        throw new MessagePackSerializationException($"Unsupported extension format {header.TypeCode}");

      try
      {
        if (type is CompoundExtensionType compoundType)
        {
          var states = new List<ValueState>();
          for (var i = 0; i < compoundType.fields.Length; i ++)
          {
            var state = Deserialize(ref reader, options);
            if (state is not ValueState valueState)
              throw new StateTypeException(typeof(ValueState), state.GetType());
            states.Add(valueState);
          }
          return new CompoundExtensionState(compoundType, states);
        }
        else if (type is RawExtensionType rawType)
        {
          if (rawType.length != header.Length)
            throw new StateException($"Expected extension length of {rawType.length}, but got {header.Length}");

          var bytes = reader.ReadRaw(header.Length).ToArray();
          return new RawExtensionState(rawType, bytes);
        }
        else
        {
          throw new MessagePackSerializationException($"Unsupported extension format {header.TypeCode}");
        }
      }
      catch (StateException ex)
      {
        throw new MessagePackSerializationException(ex.Message, ex);
      }
    }
    #endregion
  }
}
