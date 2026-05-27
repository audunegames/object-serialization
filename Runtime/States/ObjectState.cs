using System;
using System.Collections;
using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a state that contains fields of states with a <see cref="System.String"/> ma,e.
  /// </summary>
  public sealed class ObjectState : State, IObjectState, IEquatable<ObjectState>
  {
    /// <summary>
    /// The dictionary of pairs of the state.
    /// </summary>
    private readonly Dictionary<string, State> _fields;
    
    
    /// <inheritdoc/>
    public int count => _fields.Count;

    /// <inheritdoc/>
    public IEnumerable<string> keys => _fields.Keys;

    /// <inheritdoc/>
    public IEnumerable<State> values => _fields.Values;
    
    
    /// <inheritdoc/>
    int IReadOnlyCollection<KeyValuePair<string, State>>.Count => count;
    
    /// <inheritdoc/>
    State IReadOnlyDictionary<string, State>.this[string key] => ((IObjectState)this)[key];

    /// <inheritdoc/>
    IEnumerable<string> IReadOnlyDictionary<string, State>.Keys => keys;

    /// <inheritdoc/>
    IEnumerable<State> IReadOnlyDictionary<string, State>.Values => values;


    // Constructor
    public ObjectState(IEnumerable<KeyValuePair<string, State>> items = null)
    {
      if (items != null)
        _fields = new Dictionary<string, State>(items);
      else
        _fields = new Dictionary<string, State>();
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
    public State Get(string name, State defaultValue = null)
    {
      return _fields.GetValueOrDefault(name, defaultValue);
    }

    /// <inheritdoc/>
    public TState Get<TState>(string name, TState defaultValue = null) where TState : State
    {
      var state = Get(name, (State)defaultValue);
      if (state is not TState tState)
        throw new StateTypeException(typeof(TState), state.GetType());

      return tState;
    }

    /// <inheritdoc/>
    public bool TryGet(string name, out State value)
    {
      var inRange = _fields.ContainsKey(name);
      value = inRange ? _fields[name] : null;
      return inRange;
    }

    /// <inheritdoc/>
    public bool TryGet<TState>(string name, out TState value) where TState : State
    {
      if (TryGet(name, out var state) && state is TState tState)
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
    public void Set(string name, State value)
    {
      _fields[name] = value;
    }
    
    /// <inheritdoc/>
    public void Remove(string name)
    {
      _fields.Remove(name);
    }

    /// <inheritdoc/>
    public bool ContainsKey(string name)
    {
      return _fields.ContainsKey(name);
    }

    /// <inheritdoc/>
    public bool ContainsValue(State value)
    {
      return _fields.ContainsValue(value);
    }
    #endregion
    
    #region Read-only dictionary implementation
    /// <inheritdoc/>
    bool IReadOnlyDictionary<string, State>.ContainsKey(string key)
    {
      return ContainsKey(key);
    }

    /// <inheritdoc/>
    bool IReadOnlyDictionary<string, State>.TryGetValue(string key, out State value)
    {
      return TryGet(key, out value);
    }

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<string, State>> GetEnumerator()
    {
      return _fields.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _fields.GetEnumerator();
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
      return other is not null && EqualityComparer<Dictionary<string, State>>.Default.Equals(_fields, other._fields);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(_fields);
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