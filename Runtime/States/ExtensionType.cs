using System;
using System.Collections.Generic;

namespace Audune.Serialization
{
  // Class that defines an extension type
  public class ExtensionType
  {
    // The code of the extension type
    internal readonly sbyte code;


    // Constructor
    public ExtensionType(sbyte code)
    {
      this.code = code;
    }


    #region Predefined extension types
    // Static values for predefined compound extension types
    public static readonly CompoundExtensionType Vector2 = new(0x01,
      Field.Float("x"), Field.Float("y"));
    public static readonly CompoundExtensionType Vector3 = new(0x02,
      Field.Float("x"), Field.Float("y"), Field.Float("z"));
    public static readonly CompoundExtensionType Vector4 = new(0x03,
      Field.Float("x"), Field.Float("y"), Field.Float("z"), Field.Float("w"));
    public static readonly CompoundExtensionType Vector2Int = new(0x04,
      Field.Int("x"), Field.Int("y"));
    public static readonly CompoundExtensionType Vector3Int = new(0x05,
      Field.Int("x"), Field.Int("y"), Field.Int("z"));
    public static readonly CompoundExtensionType Quaternion = new(0x07,
      Field.Float("x"), Field.Float("y"), Field.Float("z"), Field.Float("w"));
    public static readonly CompoundExtensionType Rect = new(0x08,
      Field.Float("x"), Field.Float("y"), Field.Float("width"), Field.Float("height"));
    public static readonly CompoundExtensionType Bounds = new(0x09,
      Field.Float("x"), Field.Float("y"), Field.Float("z"), Field.Float("width"), Field.Float("height"), Field.Float("depth"));
    public static readonly CompoundExtensionType RectInt = new(0x0A,
      Field.Int("x"), Field.Int("y"), Field.Int("width"), Field.Int("height"));
    public static readonly CompoundExtensionType BoundsInt = new(0x0B,
      Field.Int("x"), Field.Int("y"), Field.Int("z"), Field.Int("width"), Field.Int("height"), Field.Int("depth"));

    // Static values for predefined raw extension types
    public static readonly RawExtensionType Color32 = new(0x06, 4);

    // Static values for predefined lists of extension types
    public static readonly IReadOnlyList<ExtensionType> StandardExtensionTypes = new ExtensionType[] {
      // Compound extension types
      Vector2,
      Vector3,
      Vector4,
      Vector2Int,
      Vector3Int,
      Quaternion,
      Rect,
      Bounds,
      RectInt,
      BoundsInt, 

      // Raw extension types
      Color32,
    };
    #endregion
  }
}