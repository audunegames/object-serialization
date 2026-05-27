using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines a state that contains fields of states with a <see cref="System.String"/> ma,e.
  /// </summary>
  public interface IObjectState : IReadOnlyDictionary<string, State>
  {
    /// <summary>
    /// Return the amount of fields in the state.
    /// </summary>
    public int count { get; }

    /// <summary>
    /// Return the keys of the fields in the state.
    /// </summary>
    public IEnumerable<string> keys { get; }

    /// <summary>
    /// Return the values of the fields in the state.
    /// </summary>
    public IEnumerable<State> values { get; }


    /// <summary>
    /// Return or set the field with the specified name.
    /// </summary>
    /// <param name="name">The field name to retrieve or set.</param>
    public new State this[string name] {
      get => Get(name); 
      set => Set(name, value);
    }


    /// <summary>
    /// Return the field with the specified name.
    /// </summary>
    /// <param name="name">The field name to retrieve.</param>
    /// <param name="defaultValue">The value to return when a field with name <paramref name="name"/> does not exist.</param>
    /// <returns>The field with the specified name.</returns>
    public State Get(string name, State defaultValue = null);
    
    /// <summary>
    /// Return the field with the specified name and type.
    /// </summary>
    /// <param name="name">The field name to retrieve.</param>
    /// <param name="defaultValue">The value to return when a field with name <paramref name="name"/> does not exist or when the field value is not of the specified type.</param>
    /// <typeparam name="TState">The type of the field value to get.</typeparam>
    /// <returns></returns>
    public TState Get<TState>(string name, TState defaultValue = null) where TState : State;
    
    /// <summary>
    /// Return if a field with the specified name exists and store the field value in <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The field name to retrieve.</param>
    /// <param name="value">The field value with the specified name.</param>
    /// <returns>If a field with the specified name exists.</returns>
    public bool TryGet(string name, out State value);
    
    /// <summary>
    /// Return if a field with the specified name and type exists and store the field value in <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The field name to retrieve.</param>
    /// <param name="value">The field value with the specified name.</param>
    /// <typeparam name="TState">The type of the field value to get.</typeparam>
    /// <returns>If a field with the specified name and type exists.</returns>
    public bool TryGet<TState>(string name, out TState value) where TState : State;
    
    /// <summary>
    /// Set a field with the specified name in the state.
    /// </summary>
    /// <param name="name">The field name to set.</param>
    /// <param name="value">The field value to set.</param>
    public void Set(string name, State value);
    
    /// <summary>
    /// Remove the field with the specified name from the state.
    /// </summary>
    /// <param name="name">The field name to remove.</param>
    public void Remove(string name);
    
    /// <summary>
    /// Return if the state contains a field with the specified name.
    /// </summary>
    /// <param name="name">The field name to check.</param>
    /// <returns>If the state contains a field with the specified name.</returns>
    public new bool ContainsKey(string name);
    
    /// <summary>
    /// Return if the state contains a field with the specified value.
    /// </summary>
    /// <param name="value">The field value to check.</param>
    /// <returns>If the state contains a field with the specified value.</returns>
    public bool ContainsValue(State value);
  }
}