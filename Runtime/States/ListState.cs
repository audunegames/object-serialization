using System;
using System.Collections;
using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a state that contains a list of <see cref="State"/>s.
  /// </summary>
  public sealed class ListState : State, IListState, IEquatable<ListState>
  {
    /// <summary>
    /// The list of items of the state.
    /// </summary>
    private readonly List<State> _items;
    
    
    /// <inheritdoc/>
    public int count => _items.Count;
    
    
    /// <inheritdoc/>
    int IReadOnlyCollection<State>.Count => count;
    
    /// <inheritdoc/>
    State IReadOnlyList<State>.this[int index] => ((IListState)this)[index];


    // Constructor
    public ListState(IEnumerable<State> items = null) 
    { 
      if (items != null)
        _items = new List<State>(items);
      else
        _items = new List<State>();
    }


    #region Returning and converting states
    /// <inheritdoc/>
    public override IListState AsList()
    {
      return this;
    }
    #endregion

    #region List state implementation
    /// <inheritdoc/>
    public State Get(int index, State defaultValue = null)
    {
      return index >= 0 && index < _items.Count ? _items[index] : defaultValue;
    }
    
    /// <inheritdoc/>
    public TState Get<TState>(int index, TState defaultValue = null) where TState : State
    {
      var state = Get(index, (State)defaultValue);
      if (state is not TState tState)
        throw new StateTypeException(typeof(TState), state.GetType());

      return tState;
    }

    /// <inheritdoc/>
    public bool TryGet(int index, out State value)
    {
      var inRange = index >= 0 && index < _items.Count;
      value = inRange ? _items[index] : null;
      return inRange;
    }

    /// <inheritdoc/>
    public bool TryGet<TState>(int index, out TState value) where TState : State
    {
      if (TryGet(index, out var state) && state is TState tState)
      {
        value = tState;
        return true;
      }
      else
      {
        value = null;
        return false;
      }
    }

    /// <inheritdoc/>
    public void Add(State value)
    {
      _items.Add(value);
    }

    /// <inheritdoc/>
    public void Set(int index, State value)
    {
      if (index >= 0 && index < _items.Count)
        _items[index] = value;
      else
        throw new ArgumentOutOfRangeException(nameof(index), index, $"Undefined index {index}");
    }

    /// <inheritdoc/>
    public void Remove(int index)
    {
      _items.RemoveAt(index);
    }

    /// <inheritdoc/>
    public bool Contains(State value)
    {
      return _items.Contains(value);
    }
    #endregion
    
    #region Read-only list implementation
    /// <inheritdoc/>
    public IEnumerator<State> GetEnumerator()
    {
      return _items.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _items.GetEnumerator();
    }
    #endregion

    #region Equatable implementation
    /// <inheritdoc/>
    public override bool Equals(object other)
    {
      return Equals(other as ListState);
    }

    /// <inheritdoc/>
    public bool Equals(ListState other)
    {
      return other is not null && EqualityComparer<List<State>>.Default.Equals(_items, other._items);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(_items);
    }


    /// <summary>
    /// Return if the specified <see cref="ListState"/>s are equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="ListState"/> to compare.</param>
    /// <param name="right">The right <see cref="ListState"/> to compare.</param>
    /// <returns>If the specified <see cref="ListState"/>s are equal.</returns>
    public static bool operator ==(ListState left, ListState right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Return if the specified <see cref="ListState"/>s are not equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="ListState"/> to compare.</param>
    /// <param name="right">The right <see cref="ListState"/> to compare.</param>
    /// <returns>If the specified <see cref="ListState"/>s are equal.</returns>
    public static bool operator !=(ListState left, ListState right)
    {
      return !(left == right);
    }
    #endregion
  }
}