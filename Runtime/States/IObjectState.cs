using System.Collections.Generic;

namespace Audune.Serialization
{
  // Interface that defines an object state
  public interface IObjectState : IEnumerable<KeyValuePair<string, State>>
  {
    // Return the field count of the object state
    public int count { get; }

    // Return the keys of the object state
    public IEnumerable<string> keys { get; }

    // Return the values of the object state
    public IEnumerable<State> values { get; }


    // Get and set the field with the specified name
    public State this[string name] { get => Get(name); set => Set(name, value); }


    // Get a field with the specified name
    public State Get(string name, State defaultValue = null);

    // Get a field with the specified name and state type
    public TState Get<TState>(string name, TState defaultValue = null) where TState : State;

    // Return if a field with the specified name exists and store the field value
    public bool TryGet(string name, out State value);

    // Return if a field with the specified name and state type exists and store the field value
    public bool TryGet<TState>(string name, out TState value) where TState : State;

    // Set a field with the specified name
    public void Set(string name, State value);

    // Remove the field with the specified name
    public void Remove(string name);
    
    // Return if the object contains the specified field key
    public bool ContainsKey(string name);

    // Return if the object contains the specified field value
    public bool ContainsValue(State value);
  }
}