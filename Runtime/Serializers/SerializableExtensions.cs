using System;
using System.Collections.Generic;

namespace Audune.Serialization
{
  // Class that defines extension methods for serialization
  public static class SerializableExtensions
  {
    #region Serializing and deserializing enumerables
    // Serialize all elements in an enumerable to an object state
    public static ObjectState SerializeWithKey<T>(this ISerializationContext context, IEnumerable<T> enumerable, Func<T, string> keySelector)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (enumerable == null)
          throw new ArgumentNullException(nameof(enumerable));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));

      var state = new ObjectState();
      foreach (var e in enumerable)
      {
        var key = keySelector(e);
        state.Set(key, context.Serialize(e));
      }

      return state;
    }

    // Serialize all objects in an enumerable to an object state
    public static ObjectState SerializeWithKey<T>(this ISerializationContext context, IEnumerable<T> enumerable) where T : UnityEngine.Object
    {
      return SerializeWithKey(context, enumerable, e => e.name);
    }

    // Deserialize all elements in an enumerable from an object state
    public static void DeserializeWithKey<T>(this IDeserializationContext context, IEnumerable<T> enumerable, Func<T, string> keySelector, ObjectState state)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));

      foreach (var e in enumerable)
      {
        var key = keySelector(e);
        if (state.TryGet(key, out var childState))
          context.Deserialize(childState, e);
      }
    }

    // Deserialize all objects in an enumerable from an object state
    public static void DeserializeWithKey<T>(this IDeserializationContext context, IEnumerable<T> enumerable, ObjectState state) where T : UnityEngine.Object
    {
      DeserializeWithKey(context, enumerable, e => e.name, state);
    }
    #endregion
  }
}