using System.Collections.Generic;

namespace Audune.Pickle
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

    // Return if a field with the specified name exists and store the field value
    public bool TryGet(string name, out State value);

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