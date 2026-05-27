using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Struct that defines a field in a compound extension type.
  /// </summary>
  /// <seealso cref="CompoundExtensionType"/>
  public readonly struct Field
  {
    /// <summary>
    /// The name of the field.
    /// </summary>
    public readonly string name;

    /// <summary>
    /// The type of the field.
    /// </summary>
    public readonly Type type;


    /// <summary>
    /// Constructor,
    /// </summary>
    /// <param name="name">The name of the field.</param>
    /// <param name="type">The type of the field.</param>
    public Field(string name, Type type)
    {
      this.name = name;
      this.type = type;
    }


    #region Creating fields
    /// <summary>
    /// Create a field with the specified type and name.
    /// </summary>
    /// <param name="name">The name of the field.</param>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <returns>A field with the specified type and name.</returns>
    public static Field Of<T>(string name)
    {
      return new Field(name, typeof(T));
    }

    /// <summary>
    /// Create a field with type <see cref="System.Byte"/> and the specified name.
    /// </summary>
    /// <param name="name">The name of the field.</param>
    /// <returns>A field with type <see cref="System.Byte"/> and the specified name.</returns>
    public static Field Byte(string name)
    {
      return new Field(name, typeof(byte));
    }

    /// <summary>
    /// Create a field with type <see cref="System.Int32"/> and the specified name.
    /// </summary>
    /// <param name="name">The name of the field.</param>
    /// <returns>A field with type <see cref="System.Int32"/> and the specified name.</returns>
    public static Field Int(string name)
    {
      return new Field(name, typeof(int));
    }

    /// <summary>
    /// Create a field with type <see cref="System.Single"/> and the specified name.
    /// </summary>
    /// <param name="name">The name of the field.</param>
    /// <returns>A field with type <see cref="System.Single"/> and the specified name.</returns>
    public static Field Float(string name)
    {
      return new Field(name, typeof(float));
    }
    #endregion
  }
}