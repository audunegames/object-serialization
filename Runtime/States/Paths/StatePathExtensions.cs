using System;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines extensions for state paths.
  /// </summary>
  public static class StatePathExtensions
  {
    #region State extensions
    /// <summary>
    /// Get a state with the specified path in the specified root state.
    /// </summary>
    /// <param name="root">The root state to evaluate the path on.</param>
    /// <param name="path">The path to evaluate.</param>
    /// <returns>The retrieved state at the path.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static State GetAtPath(this State root, StatePath path) 
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      return path.EvaluateGetter(root);
    }
    
    /// <summary>
    /// Get a state with the specified path and type in the specified root state.
    /// </summary>
    /// <param name="root">The root state to evaluate the path on.</param>
    /// <param name="path">The path to evaluate.</param>
    /// <typeparam name="TState">The type of the state to get.</typeparam>
    /// <returns>The retrieved state at the path.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static TState GetAtPath<TState>(this State root, StatePath path) where TState : State 
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      var state = GetAtPath(root, path);
      if (state is not TState tState)
        throw new StateTypeException(typeof(TState), state.GetType());

      return tState;
    }

    /// <summary>
    /// Return if a state with the specified path exists in the specified root state and store the state in <paramref name="value"/>.
    /// </summary>
    /// <param name="root">The root state to evaluate the path on.</param>
    /// <param name="path">The path to evaluate.</param>
    /// <param name="value">The retrieved state at the path.</param>
    /// <returns>If a state with the specified path exists in the specified root state.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static bool TryGetAtPath(this State root, StatePath path, out State value)
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      try
      {
        value = root.GetAtPath(path);
        return value != null;
      }
      catch (StateException)
      {
        value = null;
        return false;
      }
    }
    
    /// <summary>
    /// Return if a state with the specified path exists in the specified root state and store the state in <paramref name="value"/>.
    /// </summary>
    /// <param name="root">The root state to evaluate the path on.</param>
    /// <param name="path">The path to evaluate.</param>
    /// <param name="value">The retrieved state at the path.</param>
    /// <typeparam name="TState">The type of the state to get.</typeparam>
    /// <returns>If a state with the specified path exists in the specified root state.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static bool TryGetAtPath<TState>(this State root, StatePath path, out TState value) where TState : State
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      try
      {
        value = root.GetAtPath<TState>(path);
        return value != null;
      }
      catch (StateException)
      {
        value = null;
        return false;
      }
    }

    /// <summary>
    /// Set a state with the specified path in the specified root state.
    /// </summary>
    /// <param name="root">The root state to evaluate the path on.</param>
    /// <param name="path">The path to evaluate.</param>
    /// <param name="value">The state to set as the path.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static void SetAtPath(this State root, StatePath path, State value)
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      path.EvaluateSetter(root, value);
    }

    /// <summary>
    /// Return if a state with the specified path can be set in the specified root state.
    /// </summary>
    /// <param name="root">The root state to evaluate the path on.</param>
    /// <param name="path">The path to evaluate.</param>
    /// <param name="value">The state to set as the path.</param>
    /// <returns>If a state with the specified path can be set in the specified root state.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static bool TrySetAtPath(this State root, StatePath path, State value)
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      try
      {
         root.SetAtPath(path, value);
        return true;
      }
      catch (StatePathException)
      {
        return false;
      }
    }
    #endregion

    #region List and object state extensions
    /// <summary>
    /// 
    /// </summary>
    /// <param name="root"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static IEnumerable<State> GetListItemsAtPath(this State root, StatePath path)
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      return root.GetAtPath(path).IsList(out var listState) ? listState : Enumerable.Empty<State>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="root"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static IEnumerable<KeyValuePair<State, State>> GetObjectEntriesAtPath(this State root, StatePath path)
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      return root.GetAtPath(path).IsObject(out var objectState) ? objectState : Enumerable.Empty<KeyValuePair<State, State>>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="root"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static IEnumerable<State> GetObjectKeysAtPath(this State root, StatePath path)
    {
      if (root == null)
        throw new ArgumentNullException(nameof(root));
      if (path == null)
        throw new ArgumentNullException(nameof(path));
      
      return root.GetAtPath(path).IsObject(out var objectState) ? objectState.keys : Enumerable.Empty<State>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="root"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="root"/> or <paramref name="path"/> as <see langword="null"/>.</exception>
    public static IEnumerable<State> GetObjectValuesAtPath(this State root, StatePath path)
    {
      return root.GetAtPath(path).IsObject(out var objectState) ? objectState.values : Enumerable.Empty<State>();
    }
    #endregion
  }
}