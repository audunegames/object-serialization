using System;
using System.Collections.Generic;

namespace Audune.Serialization
{
  // Class that defines extension methods for serialization
  public static class SerializableExtensions
  {
    #region Serializing and deserializing dictionaries of objects
    // Serialize all persistables in a dictionary to an object state
    public static IObjectState Serialize(this IReadOnlyDictionary<string, ISerializable> dictionary, ISerializationContext context)
    {
      IObjectState state = new ObjectState();
      if (dictionary == null)
        return state;

      foreach (var e in dictionary)
        state.Set(e.Key, e.Value.Serialize(context));

      return state;
    }

    // Deserialize all persistables in a dictionary from an object state
    public static void Deserialize<TDeserializable>(this IReadOnlyDictionary<string, TDeserializable> dictionary, IObjectState state, IDeserializationContext context) where TDeserializable : IDeserializable
    {
      if (dictionary == null)
        return;

      foreach (var e in dictionary)
      {
        if (state.TryGet(e.Key, out var childState))
          e.Value.Deserialize(childState, context);
      }
    }
    #endregion
  }
}