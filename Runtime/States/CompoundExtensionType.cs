using System.Collections.Generic;

namespace Audune.Serialization
{
  // Class that defines an extension type that matches a compound extension state
  public class CompoundExtensionType : ExtensionType
  {
    // The fields of the compound extension type
    internal readonly Field[] fields;


    // Constructor
    public CompoundExtensionType(sbyte code, params Field[] fields) : base(code)
    {
      this.fields = fields;
    }


    // Return if the specified state array matches the types of the fields
    internal void AssertFields(params ValueState[] states)
    {
      // Check if the lengths match
      if (fields.Length != states.Length)
        throw new StateException($"Expected {fields.Length} states, but got {states.Length}");
      
      // Iterate over the states
      for (int i = 0; i < states.Length; i ++)
      {
        // Check if the types match
        if (!states[i].value.TryCast(fields[i].type).IsSuccesful())
          throw new StateException($"Expected value of type {fields[i].type} at index {i}, but got {states[i].value.GetType()}");
      }
    }

    // Return a dictionary that maps the names of the fields to the specified state array
    internal IDictionary<string, State> FillFields(params ValueState[] states)
    {
      // Assert the fields
      AssertFields(states);

      // Iterate over the fields and states and fill the dictionary
      var dictionary = new Dictionary<string, State>();
      for (var i = 0; i < fields.Length; i++)
        dictionary.Add(fields[i].name, states[i]);
      return dictionary;
    }
  }
}