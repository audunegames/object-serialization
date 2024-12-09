using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Audune.Pickle
{
  // State that defines an list of state items
  public sealed class ListState : State, IListState, IEquatable<ListState>
  {
    // The items of the list
    private readonly List<State> _items;


    // Constructor
    public ListState(IEnumerable<State> items = null) 
    { 
      if (items != null)
        _items = new List<State>(items);
      else
        _items = new List<State>();
    }

    #region Returning states
    // Return the state as a list state
    public override IListState AsList()
    {
      return this;
    }
    #endregion

    #region List state implementation
    // Return the item count of the list state
    public int count => _items.Count;

    
    // Get an item with the specified index
    public State Get(int index, State defaultValue)
    {
      return index >= 0 && index < _items.Count ? _items[index] : defaultValue;
    }

    // Return if an item with the specified index exists
    public bool TryGet(int index, out State value)
    {
      var inRange = index >= 0 && index < _items.Count;
      value = inRange ? ((IListState)this).Get(index) : null;
      return inRange;
    }

    // Add an item
    public void Add(State value)
    {
      _items.Add(value);
    }

    // Set an item with the specified index
    public void Set(int index, State value)
    {
      if (index >= 0 && index < _items.Count)
        _items[index] = value;
      else
        throw new ArgumentOutOfRangeException(nameof(index), index, $"Undefined index {index}");
    }

    // Remove the item with the specified index
    public void Remove(int index)
    {
      _items.RemoveAt(index);
    }

    // Return if the specified item exists
    public bool Contains(State value)
    {
      return _items.Contains(value);
    }

    // Return a generic enumerator over the values
    public IEnumerator<State> GetEnumerator()
    {
      return _items.GetEnumerator();
    }

    // Return an enumerator over the values
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _items.GetEnumerator();
    }
    #endregion

    #region Equatable implementation
    // Return if the list equals another object
    public override bool Equals(object other)
    {
      return Equals(other as ListState);
    }

    // Return if the list equals another list
    public bool Equals(ListState other)
    {
      return other is not null && EqualityComparer<List<State>>.Default.Equals(_items, other._items);
    }

    // Return the hash code of the list
    public override int GetHashCode()
    {
      return HashCode.Combine(_items);
    }


    // Return if the state equals another state using the equal operator
    public static bool operator ==(ListState left, ListState right)
    {
      return Equals(left, right);
    }

    // Return if the state does not equal another state using the not equal operator
    public static bool operator !=(ListState left, ListState right)
    {
      return !(left == right);
    }
    #endregion
  }
}