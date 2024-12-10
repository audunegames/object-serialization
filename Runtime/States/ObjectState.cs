using System;
using System.Collections;
using System.Collections.Generic;

namespace Audune.Serialization
{
  // State that defines an object containing key-state pairs
  public sealed class ObjectState : State, IObjectState, IEquatable<ObjectState>
  {
    // The dictionary of key-value pairs
    private readonly Dictionary<string, State> _fields;


    // Constructor
    public ObjectState(IEnumerable<KeyValuePair<string, State>> items = null)
    {
      if (items != null)
        _fields = new Dictionary<string, State>(items);
      else
        _fields = new Dictionary<string, State>();
    }


    #region Returning states
    // Return the state as an object state
    public override IObjectState AsObject()
    {
      return this;
    }
    #endregion

    #region Object state implementation
    // Return the field count of the object state
    public int count => _fields.Count;

    // Return the keys of the object state
    public IEnumerable<string> keys => _fields.Keys;

    // Return the values of the object state
    public IEnumerable<State> values => _fields.Values;


    // Get a field with the specified name
    public State Get(string name, State defaultValue = null)
    {
      return _fields.TryGetValue(name, out var state) ? state : defaultValue;
    }

    // Get a field with the specified name and state type
    public TState Get<TState>(string name, TState defaultValue = null) where TState : State
    {
      var state = Get(name, (State)defaultValue);
      if (state is not TState tState)
        throw new StateTypeException(typeof(TState), state.GetType());

      return tState;
    }

    // Return if a field with the specified name exists and store the item
    public bool TryGet(string name, out State value)
    {
      var inRange = _fields.ContainsKey(name);
      value = inRange ? _fields[name] : null;
      return inRange;
    }

    // Return if a field with the specified name and state type exists and store the field value
    public bool TryGet<TState>(string name, out TState value) where TState : State
    {
      if (TryGet(name, out var state) && state is TState tState)
      {
        value = tState;
        return true;
      }
      else
      {
        value = default;
        return false;
      }
    }

    // Set a field with the specified name
    public void Set(string name, State value)
    {
      _fields[name] = value;
    }
    
    // Remove the field with the specified name
    public void Remove(string name)
    {
      _fields.Remove(name);
    }

    // Return if the object contains the specified field key
    public bool ContainsKey(string name)
    {
      return _fields.ContainsKey(name);
    }

    // Return if the object contains the specified field value
    public bool ContainsValue(State value)
    {
      return _fields.ContainsValue(value);
    }

    // Return a generic enumerator over the fields
    public IEnumerator<KeyValuePair<string, State>> GetEnumerator()
    {
      return _fields.GetEnumerator();
    }

    // Return an enumerator over the fields
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _fields.GetEnumerator();
    }
    #endregion

    #region Equatable implementation
    // Return if the hash equals another object
    public override bool Equals(object other)
    {
      return Equals(other as ObjectState);
    }

    // Return if the hash equals another hash
    public bool Equals(ObjectState other)
    {
      return other is not null && EqualityComparer<Dictionary<string, State>>.Default.Equals(_fields, other._fields);
    }

    // Return the hash code of the hash
    public override int GetHashCode()
    {
      return HashCode.Combine(_fields);
    }


    // Return if the state equals another state using the equal operator
    public static bool operator ==(ObjectState left, ObjectState right)
    {
      return Equals(left, right);
    }

    // Return if the state does not equal another state using the not equal operator
    public static bool operator !=(ObjectState left, ObjectState right)
    {
      return !(left == right);
    }
    #endregion
  }
}