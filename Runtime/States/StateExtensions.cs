using System;

namespace Audune.Serialization
{
  // Class that defines extension methods for states
  public static class StateExtensions
  {
    #region Extensions for list states
    // Add a new list state to the list state and return the state
    public static ListState AddNewList(this IListState state)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var listState = new ListState();
      state.Add(listState);
      return listState;
    }

    // Add a new object state to the list state and return the state
    public static ObjectState AddNewObject(this IListState state)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var objectState = new ObjectState();
      state.Add(objectState);
      return objectState;
    }

    // Set a new list state with the specified index in the list state and return the state
    public static ListState SetNewList(this IListState state, int index)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var listState = new ListState();
      state.Set(index, listState);
      return listState;
    }

    // Set a new object state with the specified index in the list state and return the state
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
    // Set a new list state with the specified name in the object state and return the state
    public static ListState SetNewList(this IObjectState state, string name)
    {
      if (state == null)
        throw new ArgumentNullException(nameof(state));
      
      var listState = new ListState();
      state.Set(name, listState);
      return listState;
    }

    // Set a new object state with the specified name in the object state and return the state
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