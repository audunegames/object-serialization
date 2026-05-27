using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines extension methods for states.
  /// </summary>
  public static class StateExtensions
  {
    #region Extensions for list states
    /// <summary>
    /// Create a new <see cref="ListState"/> and add it to the specified list state.
    /// </summary>
    /// <param name="state">The list state to add the new <see cref="ListState"/> to.</param>
    /// <returns>The newly created <see cref="ListState"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="state"/> is <see langword="null"/>.</exception>
    public static ListState AddNewList(this IListState state)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var listState = new ListState();
      state.Add(listState);
      return listState;
    }

    /// <summary>
    /// Create a new <see cref="ObjectState"/> and add it to the specified list state.
    /// </summary>
    /// <param name="state">The list state to add the new <see cref="ObjectState"/> to.</param>
    /// <returns>The newly created <see cref="ObjectState"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="state"/> is <see langword="null"/>.</exception>
    public static ObjectState AddNewObject(this IListState state)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var objectState = new ObjectState();
      state.Add(objectState);
      return objectState;
    }
    
    /// <summary>
    /// Create a new <see cref="ListState"/> and set it with the specified index in the specified list state.
    /// </summary>
    /// <param name="state">The list state to set the new <see cref="ListState"/> in.</param>
    /// <param name="index">The index to set.</param>
    /// <returns>The newly created <see cref="ListState"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="state"/> is <see langword="null"/>.</exception>
    public static ListState SetNewList(this IListState state, int index)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var listState = new ListState();
      state.Set(index, listState);
      return listState;
    }

    /// <summary>
    /// Create a new <see cref="ObjectState"/> and set it with the specified index in the specified list state.
    /// </summary>
    /// <param name="state">The list state to set the new <see cref="ObjectState"/> in.</param>
    /// <param name="index">The index to set.</param>
    /// <returns>The newly created <see cref="ObjectState"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="state"/> is <see langword="null"/>.</exception>
    public static ObjectState SetNewObject(this IListState state, int index)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var objectState = new ObjectState();
      state.Set(index, objectState);
      return objectState;
    }
    #endregion

    #region Extensions for object states
    /// <summary>
    /// Create a new <see cref="ListState"/> and set it with the specified field name in the specified object state.
    /// </summary>
    /// <param name="state">The object state to set the new <see cref="ListState"/> in.</param>
    /// <param name="name">The field name to set.</param>
    /// <returns>The newly created <see cref="ListState"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="state"/> is <see langword="null"/>.</exception>
    public static ListState SetNewList(this IObjectState state, string name)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var listState = new ListState();
      state.Set(name, listState);
      return listState;
    }

    /// <summary>
    /// Create a new <see cref="ObjectState"/> and set it with the specified field name in the specified object state.
    /// </summary>
    /// <param name="state">The object state to set the new <see cref="ObjectState"/> in.</param>
    /// <param name="name">The field name to set.</param>
    /// <returns>The newly created <see cref="ObjectState"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="state"/> is <see langword="null"/>.</exception>
    public static ObjectState SetNewObject(this IObjectState state, string name)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var objectState = new ObjectState();
      state.Set(name, objectState);
      return objectState;
    }
    #endregion
  }
}