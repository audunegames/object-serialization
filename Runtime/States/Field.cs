using System;

namespace Audune.Pickle
{
  // Struct that defines a field with a name and type
  public readonly struct Field
  {
    // The name of the field
    public readonly string name;

    // The type of the field
    public readonly Type type;


    // Constructor
    public Field(string name, Type type)
    {
      this.name = name;
      this.type = type;
    }


    #region Creating fields
    // Create a field with the specified type and name
    public static Field Of<T>(string name)
    {
      return new Field(name, typeof(T));
    }

    // Create a byte field with the specified name
    public static Field Byte(string name)
    {
      return new Field(name, typeof(byte));
    }

    // Create an int field with the specified name
    public static Field Int(string name)
    {
      return new Field(name, typeof(int));
    }

    // Create a float field with the specified name
    public static Field Float(string name)
    {
      return new Field(name, typeof(float));
    }
    #endregion
  }
}