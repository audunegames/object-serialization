using System;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Serialization
{
  // Class that defines extensions for state paths
  public static class StatePathExtensions
  {
    #region State extensions
    // Get a state with the specified path in the specified state
    public static State GetAtPath(this State rootState, StatePath path) 
    {
      return path.EvaluateGetter(rootState);
    }

    // Return if a state with the specified path exists in the specified state
    public static bool TryGetAtPath(this State rootState, StatePath path, out State state)
    {
      try
      {
        state = rootState.GetAtPath(path);
        return state != null;
      }
      catch (StatePathException)
      {
        state = null;
        return false;
      }
    }

    // Set a state with the specified path in the specified state
    public static void SetAtPath(this State state, StatePath path, State value)
    {
      path.EvaluateSetter(state, value);
    }

    // Return if a state with the specified path can be set in the specified state
    public static bool TrySetAtPath(this State state, StatePath path, State value)
    {
      try
      {
         state.SetAtPath(path, value);
        return true;
      }
      catch (StatePathException)
      {
        return false;
      }
    }
    #endregion

    #region List and object state extensions
    // Get all items with the specified path of the specified type
    public static IEnumerable<State> GetItemsAtPath(this State state, StatePath path)
    {
      return state.GetAtPath(path).IsList(out var listState) ? listState : Enumerable.Empty<State>();
    }

    // Get all fields with the specified path of the specified type
    public static IEnumerable<KeyValuePair<string, State>> GetFieldsAtPath(this State state, StatePath path)
    {
      return state.GetAtPath(path).IsObject(out var objectState) ? objectState : Enumerable.Empty<KeyValuePair<string, State>>();
    }

    // Get all field keys with the specified path of the specified type
    public static IEnumerable<string> GetKeysAtPath(this State state, StatePath path)
    {
      return state.GetAtPath(path).IsObject(out var objectState) ? objectState.keys : Enumerable.Empty<string>();
    }

    // Get all field values with the specified path of the specified type
    public static IEnumerable<State> GetAllValuesAtPath(this State state, StatePath path)
    {
      return state.GetAtPath(path).IsObject(out var objectState) ? objectState.values : Enumerable.Empty<State>();
    }
    #endregion
  }
}