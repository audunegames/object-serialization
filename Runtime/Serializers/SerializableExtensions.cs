using System;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Serialization
{
  // Class that defines extension methods for serialization
  public static class SerializableExtensions
  {
    #region Serializing enumerables to list states
    // Serialize all objects in an enumerable to a list state
    public static ListState SerializeToList<T>(this ISerializationContext context, IEnumerable<T> enumerable)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      
      var state = new ListState();
      foreach (var e in enumerable)
        state.Add(context.Serialize(e));
      
      return state;
    }
    #endregion
    
    #region Deserializing enumerables from list states
    // Deserialize all items in a list state to an enumerable
    public static IEnumerable<T> DeserializeFromList<T>(this IDeserializationContext context, IListState state)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      foreach (var e in state)
        yield return context.Deserialize<T>(e);
    }

    
    // Deserialize all items in a list state and add them into the specified collection
    public static void AddDeserializationFromList<T>(this IDeserializationContext context, IListState state, ICollection<T> collection)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      if (collection == null)
        throw new ArgumentNullException(nameof(collection));
      
      foreach (var e in DeserializeFromList<T>(context, state))
        collection.Add(e);
    }
    
    // Deserialize all items in a list state and add them into the specified dictionary with the specified key selector
    public static void AddDeserializationFromList<T, TKey>(this IDeserializationContext context, IListState state, IDictionary<TKey, T> dictionary, Func<T, TKey> keySelector)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      if (dictionary == null)
        throw new ArgumentNullException(nameof(dictionary));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));
      
      foreach (var e in DeserializeFromList<T>(context, state))
        dictionary.Add(keySelector(e), e);
    }
    #endregion
    
    #region Serializing enumerables to object states
    // Serialize all objects in an enumerable to an object state
    public static ObjectState SerializeToObject<T>(this ISerializationContext context, IEnumerable<KeyValuePair<string, T>> enumerable)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      
      var state = new ObjectState();
      foreach (var e in enumerable)
        state.Set(e.Key, context.Serialize(e.Value));
      
      return state;
    }
    
    // Serialize all objects in an enumerable to an object state by using the specified key selector
    public static ObjectState SerializeToObject<T>(this ISerializationContext context, IEnumerable<T> enumerable, Func<T, string> keySelector)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));
      
      return SerializeToObject(context, enumerable.Select(e => KeyValuePair.Create(keySelector(e), e)));
    }
    
    // Serialize all Unity objects in an enumerable to an object state
    public static ObjectState SerializeToObject<T>(this ISerializationContext context, IEnumerable<T> enumerable) where T : UnityEngine.Object
    {
      return SerializeToObject(context, enumerable, e => e.name);
    }
    #endregion
    
    #region DeDeserializing enumerables from object states
    // Deserialize all items in an object state to an enumerable
    public static IEnumerable<KeyValuePair<string, T>> DeserializeFromObject<T>(this IDeserializationContext context, IObjectState state)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      foreach (var e in state)
        yield return KeyValuePair.Create(e.Key, context.Deserialize<T>(e.Value));
    }
    
    
    // Deserialize all items in an object state and add them into the specified collection
    public static void AddDeserializationFromObject<T>(this IDeserializationContext context, IObjectState state, ICollection<KeyValuePair<string, T>> collection)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      if (collection == null)
        throw new ArgumentNullException(nameof(collection));
      
      foreach (var e in DeserializeFromObject<T>(context, state))
        collection.Add(e);
    }
    
    // Deserialize all items in an object state and add them into the specified dictionary with the specified key selector
    public static void AddDeserializationFromObject<T, TKey>(this IDeserializationContext context, IObjectState state, IDictionary<TKey, T> dictionary, Func<string, TKey> keySelector)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      if (dictionary == null)
        throw new ArgumentNullException(nameof(dictionary));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));
      
      foreach (var e in DeserializeFromObject<T>(context, state))
        dictionary.Add(keySelector(e.Key), e.Value);
    }
    
    
    // Deserialize all items in an object state and apply them to the specified enumerable of objects
    public static void ApplyDeserializationFromObject<T>(this IDeserializationContext context, IObjectState state, IEnumerable<T> enumerable, Func<T, string> keySelector)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
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

    // Deserialize all items in an object state and apply them to the specified enumerable of Unity objects
    public static void ApplyDeserializationFromObject<T>(this IDeserializationContext context, ObjectState state, IEnumerable<T> enumerable) where T : UnityEngine.Object
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      
      ApplyDeserializationFromObject(context, state, enumerable, e => e.name);
    }
    #endregion
    
    #region Obsolete methods
    [Obsolete("Use SerializeToObject instead", false)]
    public static ObjectState SerializeWithKey<T>(this ISerializationContext context, IEnumerable<T> enumerable, Func<T, string> keySelector)
    {
      return SerializeToObject(context, enumerable, keySelector);
    }

    [Obsolete("Use SerializeToObject instead", false)]
    public static ObjectState SerializeWithKey<T>(this ISerializationContext context, IEnumerable<T> enumerable) where T : UnityEngine.Object
    {
      return SerializeToObject(context, enumerable);
    }

    [Obsolete("Use ApplyDeserializationFromObject instead", false)]
    public static void DeserializeWithKey<T>(this IDeserializationContext context, IEnumerable<T> enumerable, Func<T, string> keySelector, ObjectState state)
    {
      ApplyDeserializationFromObject(context, state, enumerable, keySelector);
    }

    [Obsolete("Use ApplyDeserializationFromObject instead", false)]
    public static void DeserializeWithKey<T>(this IDeserializationContext context, IEnumerable<T> enumerable, ObjectState state) where T : UnityEngine.Object
    {
      ApplyDeserializationFromObject(context, state, enumerable);
    }
    #endregion
  }
}