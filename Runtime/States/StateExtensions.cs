namespace Audune.Pickle
{
  // Class that defines extension methods for states
  public static class StateExtensions
  {
    #region Extensions for list states
    // Add a new list state to the list state and return the state
    public static ListState AddNewList(this IListState state)
    {
      var listState = new ListState();
      state.Add(listState);
      return listState;
    }

    // Add a new object state to the list state and return the state
    public static ObjectState AddNewObject(this IListState state)
    {
      var objectState = new ObjectState();
      state.Add(objectState);
      return objectState;
    }

    // Set a new list state with the specified index in the list state and return the state
    public static IListState SetNewList(this IListState state, int index)
    {
      var listState = new ListState();
      state.Set(index, listState);
      return listState;
    }

    // Set a new object statewith the specified index in the list stateand return the state
    public static IObjectState SetNewObject(this IListState state, int index)
    {
      var objectState = new ObjectState();
      state.Set(index, objectState);
      return objectState;
    }
    #endregion

    #region Extensions for object states
    // Set a new list state with the specified name in the object state and return the state
    public static IListState SetNewList(this IObjectState state, string name)
    {
      var listState = new ListState();
      state.Set(name, listState);
      return listState;
    }

    // Set a new object state with the specified name in the object state and return the state
    public static IObjectState SetNewObject(this IObjectState state, string name)
    {
      var objectState = new ObjectState();
      state.Set(name, objectState);
      return objectState;
    }
    #endregion
  }
}