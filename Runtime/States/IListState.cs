using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines a state that contains a list of <see cref="State"/>s.
  /// </summary>
  public interface IListState : IReadOnlyList<State>
  {
    /// <summary>
    /// Return the amount of items in the state.
    /// </summary>
    public int count { get; }


    /// <summary>
    /// Return or set the item with the specified index.
    /// </summary>
    /// <param name="index">The index to retrieve or set.</param>
    public new State this[int index] {
      get => Get(index);
      set => Set(index, value);
    }


    /// <summary>
    /// Return the item with the specified index.
    /// </summary>
    /// <param name="index">The index to retrieve.</param>
    /// <param name="defaultValue">The value to return when an item with index <paramref name="index"/> does not exist.</param>
    /// <returns>The item with the specified index.</returns>
    public State Get(int index, State defaultValue = null);
    
    /// <summary>
    /// Return the item with the specified index and specified type.
    /// </summary>
    /// <param name="index">The index to retrieve.</param>
    /// <param name="defaultValue">The value to return when an item with index <paramref name="index"/> does not exist or when the item is not of the specified type.</param>
    /// <typeparam name="TState">The type of the item to get.</typeparam>
    /// <returns></returns>
    public TState Get<TState>(int index, TState defaultValue = null) where TState : State;
    
    /// <summary>
    /// Return if an item with the specified index exists and store the item in <paramref name="value"/>.
    /// </summary>
    /// <param name="index">The index to retrieve.</param>
    /// <param name="value">The item with the specified index.</param>
    /// <returns>If an item with the specified index exists.</returns>
    public bool TryGet(int index, out State value);
    
    /// <summary>
    /// Return if an item with the specified index and type exists and store the item in <paramref name="value"/>.
    /// </summary>
    /// <param name="index">The index to retrieve.</param>
    /// <param name="value">The item with the specified index and type.</param>
    /// <typeparam name="TState"></typeparam>
    /// <returns>If an item with the specified index and type exists.</returns>
    public bool TryGet<TState>(int index, out TState value) where TState : State;

    /// <summary>
    /// Add an item to the state.
    /// </summary>
    /// <param name="value">The state to add.</param>
    public void Add(State value);
    
    /// <summary>
    /// Set an item with the specified index in the state.
    /// </summary>
    /// <param name="index">The index to set.</param>
    /// <param name="value">The state to set.</param>
    public void Set(int index, State value);
    
    /// <summary>
    /// Remove the item with the specified index from the state.
    /// </summary>
    /// <param name="index">The index to remove.</param>
    public void Remove(int index);
    
    /// <summary>
    /// Return if the state contains the specified value.
    /// </summary>
    /// <param name="value">The state to check.</param>
    /// <returns>If the state contains the specified value.</returns>
    public bool Contains(State value);
  }
}