using System;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines extension methods for serialization.
  /// </summary>
  public static class SerializableExtensions
  {
    #region Serializing enumerables to list states
    /// <summary>
    /// Serialize all objects in the specified enumerable to a list state.
    /// </summary>
    /// <param name="context">The context for serialization.</param>
    /// <param name="enumerable">The enumerable containing the objects to serialize.</param>
    /// <typeparam name="T">The type of the values to serialize.</typeparam>
    /// <returns>A list state containing the serialized states.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="enumerable"/>, are <see langword="null"/>.</exception>
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
    /// <summary>
    /// Deserialize all items in the specified list state to an enumerable.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <typeparam name="T">The type of the values to deserialize.</typeparam>
    /// <returns>An enumerable containing the deserialized objects.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="state"/> are <see langword="null"/>.</exception>
    public static IEnumerable<T> DeserializeFromList<T>(this IDeserializationContext context, IListState state)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      foreach (var e in state)
        yield return context.Deserialize<T>(e);
    }

    
    /// <summary>
    /// Deserialize all items in the specified list state and add them to the specified collection.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="collection">The collection to add the deserialized objects to.</param>
    /// <typeparam name="T">The type of the values to deserialize.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, or <paramref name="collection"/> are <see langword="null"/>.</exception>
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
    
    /// <summary>
    /// Deserialize all items in the specified list state and add them to the specified dictionary with the specified key selector.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="dictionary">The dictionary to add the deserialized objects to.</param>
    /// <param name="keySelector">A function that defines how to select a key from a deserialized object.</param>
    /// <typeparam name="T">The type of the values to deserialize.</typeparam>
    /// <typeparam name="TKey">The type of the key of the dictionary.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, <paramref name="dictionary"/>, or <paramref name="keySelector"/> are <see langword="null"/>.</exception>
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
    /// <summary>
    /// Serialize all objects in the specified enumerable to an object state.
    /// </summary>
    /// <param name="context">The context for serialization.</param>
    /// <param name="enumerable">The enumerable containing key-value pairs that contain the objects to serialize.</param>
    /// <typeparam name="T">The type of the values to serialize.</typeparam>
    /// <returns>An object state containing the serialized states.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="enumerable"/> are <see langword="null"/>.</exception>
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
    
    /// <summary>
    /// Serialize all objects in the specified enumerable to an object state by using the specified key selector.
    /// </summary>
    /// <remarks>
    /// Use <see cref="ApplyDeserializationFromObject{T}(IDeserializationContext, IObjectState, IEnumerable{T}, Func{T,string})"/> to deserialize an enumerable of objects.
    /// </remarks>
    /// <param name="context">The context for serialization.</param>
    /// <param name="enumerable">The enumerable containing the objects to serialize.</param>
    /// <param name="keySelector">A function that defines how to select a key from an object to serialize.</param>
    /// <typeparam name="T">The type of the values to serialize.</typeparam>
    /// <returns>An object state containing the serialized states.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="enumerable"/>, or <paramref name="keySelector"/> are <see langword="null"/>.</exception>
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
    
    /// <summary>
    /// Serialize all Unity objects in the specified enumerable to an object state.
    /// </summary>
    /// <remarks>
    /// Use <see cref="ApplyDeserializationFromObject{T}(IDeserializationContext, IObjectState, IEnumerable{T})"/> to deserialize an enumerable of Unity objects.
    /// </remarks>
    /// <param name="context">The context for serialization.</param>
    /// <param name="enumerable">The enumerable containing the Unity objects to serialize.</param>
    /// <typeparam name="T">The type of the Unity objects to serialize.</typeparam>
    /// <returns>An object state containing the serialized states.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="enumerable"/> are <see langword="null"/>.</exception>
    public static ObjectState SerializeToObject<T>(this ISerializationContext context, IEnumerable<T> enumerable) where T : UnityEngine.Object
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      
      return SerializeToObject(context, enumerable, e => e.name);
    }
    #endregion
    
    #region Deserializing enumerables from object states
    /// <summary>
    /// Deserialize all items in the specified object state to an enumerable.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <typeparam name="T">The type of the values to deserialize.</typeparam>
    /// <returns>An enumerable containing key-value pairs that contain the deserialized objects.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="state"/> are <see langword="null"/>.</exception>
    public static IEnumerable<KeyValuePair<string, T>> DeserializeFromObject<T>(this IDeserializationContext context, IObjectState state)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      foreach (var e in state)
        yield return KeyValuePair.Create(e.Key, context.Deserialize<T>(e.Value));
    }
    
    
    /// <summary>
    /// Deserialize all items in the specified object state and add them to the specified collection.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="collection">The collection to add the deserialized objects to.</param>
    /// <typeparam name="T">The type of the values to deserialize.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, or <paramref name="collection"/> are <see langword="null"/>.</exception>
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
    
    /// <summary>
    /// Deserialize all items in the specified object state and add them to the specified dictionary with the specified key selector.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="dictionary">The dictionary to add the deserialized objects to.</param>
    /// <param name="keySelector">A function that defines how to select a key from a name.</param>
    /// <typeparam name="T">The type of the values to deserialize.</typeparam>
    /// <typeparam name="TKey">The type of the key of the dictionary.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, <paramref name="dictionary"/>, or <paramref name="keySelector"/> are <see langword="null"/>.</exception>
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
    
    
    /// <summary>
    /// Deserialize all items in the specified object state and apply them to the specified enumerable of objects.
    /// </summary>
    /// <remarks>
    /// Use <see cref="SerializeToObject{T}(ISerializationContext, IEnumerable{T}, Func{T, string})"/> to serialize an enumerable of objects.
    /// </remarks>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="enumerable">The enumerable of objects to apply the deserialized states to based on their key.</param>
    /// <param name="keySelector">A function that defines how to select a key from an object in the enumerable.</param>
    /// <typeparam name="T">The type of the values to deserialize.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, <paramref name="enumerable"/>, or <paramref name="keySelector"/> are <see langword="null"/>.</exception>
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

    /// <summary>
    /// Deserialize all items in the specified object state and apply them to the specified enumerable of Unity objects.
    /// </summary>
    /// <remarks>
    /// Use <see cref="SerializeToObject{T}(ISerializationContext, IEnumerable{T})"/> to serialize an enumerable of Unity objects.
    /// </remarks>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="enumerable">The enumerable of Unity objects to apply the deserialized states to based on their key.</param>
    /// <typeparam name="T">The type of the Unity objects to deserialize.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, or <paramref name="enumerable"/> are <see langword="null"/>.</exception>
    public static void ApplyDeserializationFromObject<T>(this IDeserializationContext context, IObjectState state, IEnumerable<T> enumerable) where T : UnityEngine.Object
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