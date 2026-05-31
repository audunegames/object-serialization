using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines a state that contains key-value pairs of states.
  /// </summary>
  public interface IObjectState : IReadOnlyDictionary<State, State>
  {
    /// <summary>
    /// Return the amount of entries in the state.
    /// </summary>
    public int count { get; }

    /// <summary>
    /// Return the keys of the entries in the state.
    /// </summary>
    public IEnumerable<State> keys { get; }

    /// <summary>
    /// Return the values of the entries in the state.
    /// </summary>
    public IEnumerable<State> values { get; }

    
    /// <summary>
    /// Return or set the value with the specified key.
    /// </summary>
    /// <param name="key">The key to retrieve or set.</param>
    public new State this[State key] {
      get => Get(key); 
      set => Set(key, value);
    }

    
    /// <summary>
    /// Return the value with the specified key.
    /// </summary>
    /// <param name="key">The key to retrieve.</param>
    /// <param name="defaultValue">The value to return when a value with key <paramref name="key"/> does not exist.</param>
    /// <returns>The value with the specified key.</returns>
    public State Get(State key, State defaultValue = null);
    
    /// <summary>
    /// Return the value with the specified key and type.
    /// </summary>
    /// <param name="key">The key to retrieve.</param>
    /// <param name="defaultValue">The value to return when a value with key <paramref name="key"/> does not exist or when the value is not of the specified type.</param>
    /// <typeparam name="TState">The type of the value to get.</typeparam>
    /// <returns>The value with the specified key.</returns>
    public TState Get<TState>(State key, TState defaultValue = null) where TState : State;
    
    /// <summary>
    /// Return if a value with the specified key exists and store the value in <paramref name="value"/>.
    /// </summary>
    /// <param name="key">The key to retrieve.</param>
    /// <param name="value">The value with the specified key.</param>
    /// <returns>If a value with the specified key exists.</returns>
    public bool TryGet(State key, out State value);
    
    /// <summary>
    /// Return if a value with the specified key and type exists and store the value in <paramref name="value"/>.
    /// </summary>
    /// <param name="key">The key to retrieve.</param>
    /// <param name="value">The value with the specified key.</param>
    /// <typeparam name="TState">The type of the value to get.</typeparam>
    /// <returns>If a value with the specified key and type exists.</returns>
    public bool TryGet<TState>(State key, out TState value) where TState : State;
    
    /// <summary>
    /// Set a value with the specified key in the state.
    /// </summary>
    /// <param name="key">The key to set.</param>
    /// <param name="value">The value to set.</param>
    public void Set(State key, State value);
    
    /// <summary>
    /// Remove the value with the specified key from the state.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    public void Remove(State key);
    
    /// <summary>
    /// Return if the state contains a value with the specified key
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>If the state contains a value with the specified key.</returns>
    public new bool ContainsKey(State key);
    
    /// <summary>
    /// Return if the state contains a field with the specified value.
    /// </summary>
    /// <param name="value">The field value to check.</param>
    /// <returns>If the state contains a field with the specified value.</returns>
    public bool ContainsValue(State value);
  }
}