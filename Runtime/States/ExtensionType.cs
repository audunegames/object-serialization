using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an extension type.
  /// </summary>
  public class ExtensionType
  {
    /// <summary>
    /// The code of the extension type.
    /// </summary>
    internal readonly sbyte code;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="code"> The code of the extension type.</param>
    public ExtensionType(sbyte code)
    {
      this.code = code;
    }


    #region Predefined extension types
    /// <summary>
    /// Extension type adapter for <see cref="System.DateTime"/> objects.
    /// </summary>
    public static readonly TimestampExtensionType Timestamp = new();
    
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Vector2"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType Vector2 = new(0x01,
      Field.Float("x"), Field.Float("y"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Vector3"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType Vector3 = new(0x02,
      Field.Float("x"), Field.Float("y"), Field.Float("z"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Vector4"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType Vector4 = new(0x03,
      Field.Float("x"), Field.Float("y"), Field.Float("z"), Field.Float("w"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Vector2Int"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType Vector2Int = new(0x04,
      Field.Int("x"), Field.Int("y"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Vector3Int"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType Vector3Int = new(0x05,
      Field.Int("x"), Field.Int("y"), Field.Int("z"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Quaternion"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType Quaternion = new(0x07,
      Field.Float("x"), Field.Float("y"), Field.Float("z"), Field.Float("w"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Rect"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType Rect = new(0x08,
      Field.Float("x"), Field.Float("y"), Field.Float("width"), Field.Float("height"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Bounds"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType Bounds = new(0x09,
      Field.Float("x"), Field.Float("y"), Field.Float("z"), Field.Float("width"), Field.Float("height"), Field.Float("depth"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.RectInt"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType RectInt = new(0x0A,
      Field.Int("x"), Field.Int("y"), Field.Int("width"), Field.Int("height"));
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.BoundsInt"/> objects.
    /// </summary>
    public static readonly CompoundExtensionType BoundsInt = new(0x0B,
      Field.Int("x"), Field.Int("y"), Field.Int("z"), Field.Int("width"), Field.Int("height"), Field.Int("depth"));
    
    
    /// <summary>
    /// Extension type adapter for <see cref="UnityEngine.Color32"/> objects.
    /// </summary>
    public static readonly RawExtensionType Color32 = new(0x06, 4);
    
    /// <summary>
    /// Extension type adapter for <see cref="System.Guid"/> objects.
    /// </summary>
    public static readonly RawExtensionType Guid = new(0x10, 16);


    /// <summary>
    /// List that defines the standard extension types for a serializer.
    /// </summary>
    public static readonly IReadOnlyList<ExtensionType> StandardExtensionTypes = new ExtensionType[] {
      Timestamp,
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
      Color32,
      Guid,
    };
    #endregion
  }
}