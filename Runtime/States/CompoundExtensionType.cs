using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an extension type for a <see cref="CompoundExtensionState"/>.
  /// </summary>
  public class CompoundExtensionType : ExtensionType
  {
    /// <summary>
    /// The fields of the compound extension type,
    /// </summary>
    internal readonly Field[] fields;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="code">The code of the extension type.</param>
    /// <param name="fields">The fields if the extension type.</param>
    public CompoundExtensionType(sbyte code, params Field[] fields) : base(code)
    {
      this.fields = fields;
    }


    /// <summary>
    /// Assert that the specified array of value states matches the fields of the extension type.
    /// </summary>
    /// <param name="states">The array of value states to match.</param>
    /// <exception cref="StateException">IF the specified array of value states do not match the fields of the extension type.</exception>
    internal void AssertFields(params ValueState[] states)
    {
      // Check if the lengths match
      if (fields.Length != states.Length)
        throw new StateException($"Expected {fields.Length} states, but got {states.Length}");
      
      // Iterate over the states
      for (var i = 0; i < states.Length; i ++)
      {
        // Check if the types match
        if (!states[i].value.TryCast(fields[i].type).IsSuccessful())
          throw new StateException($"Expected value of type {fields[i].type} at index {i}, but got {states[i].value.GetType()}");
      }
    }
    
    /// <summary>
    /// Return a dictionary that maps the field names of the extension type to the specified array of value states.
    /// </summary>
    /// <param name="states">The array of value states to map the field names to.</param>
    /// <returns>A dictionary that maps the field names of the extension type to the specified array of value states.</returns>
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