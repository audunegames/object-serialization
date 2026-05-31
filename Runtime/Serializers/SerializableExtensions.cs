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
    /// <typeparam name="TKey">The type of the keys to serialize.</typeparam>
    /// <typeparam name="TValue">The type of the values to serialize.</typeparam>
    /// <returns>An object state containing the serialized states.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="enumerable"/> are <see langword="null"/>.</exception>
    public static ObjectState SerializeToObject<TKey, TValue>(this ISerializationContext context, IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      
      var state = new ObjectState();
      foreach (var e in enumerable)
      {
        var keyState = context.Serialize(e.Key);
        var valueState = context.Serialize(e.Value);
        state.Set(keyState, valueState);
      }

      return state;
    }
    
    /// <summary>
    /// Serialize all objects in the specified enumerable to an object state by using the specified key selector.
    /// </summary>
    /// <remarks>
    /// Use <see cref="ApplyDeserializationFromObject{TKey,TValue}(IDeserializationContext,IObjectState,IEnumerable{TValue},Func{TValue,TKey})"/> to deserialize an enumerable of objects.
    /// </remarks>
    /// <param name="context">The context for serialization.</param>
    /// <param name="enumerable">The enumerable containing the objects to serialize.</param>
    /// <param name="keySelector">A function that defines how to select a key from an object to serialize.</param>
    /// <typeparam name="TKey">The type of the keys to serialize.</typeparam>
    /// <typeparam name="TValue">The type of the values to serialize.</typeparam>
    /// <returns>An object state containing the serialized states.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="enumerable"/>, or <paramref name="keySelector"/> are <see langword="null"/>.</exception>
    public static ObjectState SerializeToObject<TKey, TValue>(this ISerializationContext context, IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));
      
      return SerializeToObject(context, enumerable.Select(e => KeyValuePair.Create(keySelector(e), e)));
    }
    #endregion
    
    #region Deserializing enumerables from object states
    /// <summary>
    /// Deserialize all items in the specified object state to an enumerable.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <typeparam name="TKey">The type of the keys to serialize.</typeparam>
    /// <typeparam name="TValue">The type of the values to deserialize.</typeparam>
    /// <returns>An enumerable containing key-value pairs that contain the deserialized objects.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="state"/> are <see langword="null"/>.</exception>
    public static IEnumerable<KeyValuePair<TKey, TValue>> DeserializeFromObject<TKey, TValue>(this IDeserializationContext context, IObjectState state)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));

      foreach (var e in state)
      {
        var key = context.Deserialize<TKey>(e.Key);
        var value = context.Deserialize<TValue>(e.Value);
        yield return KeyValuePair.Create(key, value);
      }
    }
    
    
    /// <summary>
    /// Deserialize all items in the specified object state and add them to the specified collection.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="collection">The collection to add the deserialized objects to.</param>
    /// <typeparam name="TKey">The type of the keys to serialize.</typeparam>
    /// <typeparam name="TValue">The type of the values to deserialize.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, or <paramref name="collection"/> are <see langword="null"/>.</exception>
    public static void AddDeserializationFromObject<TKey, TValue>(this IDeserializationContext context, IObjectState state, ICollection<KeyValuePair<TKey, TValue>> collection)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      if (collection == null)
        throw new ArgumentNullException(nameof(collection));
      
      foreach (var e in DeserializeFromObject<TKey, TValue>(context, state))
        collection.Add(e);
    }
    
    /// <summary>
    /// Deserialize all items in the specified object state and add them to the specified dictionary with the specified key selector.
    /// </summary>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="dictionary">The dictionary to add the deserialized objects to.</param>
    /// <typeparam name="TKey">The type of the keys to serialize.</typeparam>
    /// <typeparam name="TValue">The type of the values to deserialize.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, or <paramref name="dictionary"/> are <see langword="null"/>.</exception>
    public static void AddDeserializationFromObject<TValue, TKey>(this IDeserializationContext context, IObjectState state, IDictionary<TKey, TValue> dictionary)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      if (dictionary == null)
        throw new ArgumentNullException(nameof(dictionary));
      
      foreach (var e in DeserializeFromObject<TKey, TValue>(context, state))
        dictionary.Add(e.Key, e.Value);
    }
    
    
    /// <summary>
    /// Deserialize all items in the specified object state and apply them to the specified enumerable of objects.
    /// </summary>
    /// <remarks>
    /// Use <see cref="SerializeToObject{TKey,TValue}(ISerializationContext,IEnumerable{TValue},Func{TValue,TKey})"/> to serialize an enumerable of objects.
    /// </remarks>
    /// <param name="context">The context for deserialization.</param>
    /// <param name="state">The state to deserialize.</param>
    /// <param name="enumerable">The enumerable of objects to apply the deserialized states to based on their key.</param>
    /// <param name="keySelector">A function that defines how to select a key from an object in the enumerable.</param>
    /// <typeparam name="TKey">The type of the keys to serialize.</typeparam>
    /// <typeparam name="TValue">The type of the values to deserialize.</typeparam>
    /// <exception cref="ArgumentNullException">If <paramref name="context"/>, <paramref name="state"/>, <paramref name="enumerable"/>, or <paramref name="keySelector"/> are <see langword="null"/>.</exception>
    public static void ApplyDeserializationFromObject<TKey, TValue>(this IDeserializationContext context, IObjectState state, IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      if (enumerable == null)
        throw new ArgumentNullException(nameof(enumerable));
      if (keySelector == null)
        throw new ArgumentNullException(nameof(keySelector));
      
      var dictionary = state
        .Select(e => KeyValuePair.Create(context.Deserialize<TKey>(e.Key), e.Value))
        .ToDictionary(e => e.Key, e => e.Value);
      
      foreach (var e in enumerable)
      {
        var key = keySelector(e);
        if (dictionary.TryGetValue(key, out var valueState))
          context.Deserialize(valueState, e);
      }
    }
    #endregion
  }
}