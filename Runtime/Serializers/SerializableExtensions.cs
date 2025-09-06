using System;
using System.Collections.Generic;

namespace Audune.Serialization
{
  // Class that defines extension methods for serialization
  public static class SerializableExtensions
  {
    #region Serializing and deserializing dictionaries of objects
    // Serialize all elements in an enumerable to an object state
    public static ObjectState Serialize<TSerializable>(this IEnumerable<TSerializable> enumerable, Func<TSerializable, string> keySelector, ISerializationContext context) where TSerializable : ISerializable
    {
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));

      var state = new ObjectState();
      foreach (var e in enumerable)
      {
        var key = keySelector(e);
        state.Set(key, e.Serialize(context));
      }

      return state;
    }

    // Serialize all objects in an enumerable to an object state
    public static ObjectState Serialize<TSerializable>(this IEnumerable<TSerializable> enumerable, ISerializationContext context) where TSerializable : UnityEngine.Object, ISerializable
    {
      return Serialize(enumerable, e => e.name, context);
    }

    // Deserialize all elements in an enumerable from an object state
    public static void Deserialize<TDeserializable>(this IEnumerable<TDeserializable> enumerable, Func<TDeserializable, string> keySelector, ObjectState state, IDeserializationContext context) where TDeserializable : IDeserializable
    {
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));

      foreach (var e in enumerable)
      {
        var key = keySelector(e);
        if (state.TryGet(key, out var childState))
          e.Deserialize(childState, context);
      }
    }

    // Deserialize all objects in an enumerable from an object state
    public static void Deserialize<TDeserializable>(this IEnumerable<TDeserializable> enumerable, ObjectState state, IDeserializationContext context) where TDeserializable : UnityEngine.Object, IDeserializable
    {
      Deserialize(enumerable, e => e.name, state, context);
    }
    #endregion
  }
}