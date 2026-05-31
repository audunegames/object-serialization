using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a state that contains key-value pairs of states.
  /// </summary>
  public sealed class ObjectState : State, IObjectState, IEquatable<ObjectState>
  {
    /// <summary>
    /// The dictionary of items of the state.
    /// </summary>
    private readonly Dictionary<State, State> _items;
    
    
    /// <inheritdoc/>
    public int count => _items.Count;

    /// <inheritdoc/>
    public IEnumerable<State> keys => _items.Keys;

    /// <inheritdoc/>
    public IEnumerable<State> values => _items.Values;
    
    
    /// <inheritdoc/>
    int IReadOnlyCollection<KeyValuePair<State, State>>.Count => count;
    
    /// <inheritdoc/>
    State IReadOnlyDictionary<State, State>.this[State key] => ((IObjectState)this)[key];

    /// <inheritdoc/>
    IEnumerable<State> IReadOnlyDictionary<State, State>.Keys => keys;

    /// <inheritdoc/>
    IEnumerable<State> IReadOnlyDictionary<State, State>.Values => values;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="enumerable">The enumerable whose items are copied to the new state.</param>
    public ObjectState(IEnumerable<KeyValuePair<State, State>> enumerable = null)
    {
      if (enumerable != null)
        _items = new Dictionary<State, State>(enumerable);
      else
        _items = new Dictionary<State, State>();
    }


    #region Returning and converting states
    /// <inheritdoc/>
    public override IObjectState AsObject()
    {
      return this;
    }
    #endregion

    #region Object state implementation
    /// <inheritdoc/>
    public State Get(State key, State defaultValue = null)
    {
      return _items.GetValueOrDefault(key, defaultValue);
    }

    /// <inheritdoc/>
    public TState Get<TState>(State key, TState defaultValue = null) where TState : State
    {
      var state = Get(key, (State)defaultValue);
      if (state is not TState tState)
        throw new StateTypeException(typeof(TState), state.GetType());

      return tState;
    }

    /// <inheritdoc/>
    public bool TryGet(State key, out State value)
    {
      var inRange = _items.ContainsKey(key);
      value = inRange ? _items[key] : null;
      return inRange;
    }

    /// <inheritdoc/>
    public bool TryGet<TState>(State key, out TState value) where TState : State
    {
      if (TryGet(key, out var state) && state is TState tState)
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
    public void Set(State key, State value)
    {
      _items[key] = value;
    }
    
    /// <inheritdoc/>
    public void Remove(State key)
    {
      _items.Remove(key);
    }

    /// <inheritdoc/>
    public bool ContainsKey(State key)
    {
      return _items.ContainsKey(key);
    }

    /// <inheritdoc/>
    public bool ContainsValue(State value)
    {
      return _items.ContainsValue(value);
    }
    #endregion
    
    #region Read-only dictionary implementation
    /// <inheritdoc/>
    bool IReadOnlyDictionary<State, State>.ContainsKey(State key)
    {
      return ContainsKey(key);
    }

    /// <inheritdoc/>
    bool IReadOnlyDictionary<State, State>.TryGetValue(State key, out State value)
    {
      return TryGet(key, out value);
    }

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<State, State>> GetEnumerator()
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
      return Equals(other as ObjectState);
    }

    /// <inheritdoc/>
    public bool Equals(ObjectState other)
    {
      return other is not null && EqualityComparer<Dictionary<State, State>>.Default.Equals(_items, other._items);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(_items);
    }


    /// <summary>
    /// Return if the specified <see cref="ObjectState"/>s are equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="ObjectState"/> to compare.</param>
    /// <param name="right">The right <see cref="ObjectState"/> to compare.</param>
    /// <returns>If the specified <see cref="ObjectState"/>s are equal.</returns>
    public static bool operator ==(ObjectState left, ObjectState right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Return if the specified <see cref="ObjectState"/>s are not equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="ObjectState"/> to compare.</param>
    /// <param name="right">The right <see cref="ObjectState"/> to compare.</param>
    /// <returns>If the specified <see cref="ObjectState"/>s are equal.</returns>
    public static bool operator !=(ObjectState left, ObjectState right)
    {
      return !(left == right);
    }
    #endregion
  }
}