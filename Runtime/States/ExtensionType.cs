using System.Collections.Generic;

namespace Audune.Pickle
{
  // Class that defines an extension type
  public class ExtensionType
  {
    // The code of the extension type
    private readonly sbyte _code;

   

    // Return the code of the extension type
    internal sbyte code => _code;


    // Constructor
    public ExtensionType(sbyte code)
    {
      _code = code;
    }


    #region Predefined extension types
    // Static values for predefined compound extension types
    public static readonly CompoundExtensionType Vector2 = new(0x01, typeof(float), 2);
    public static readonly CompoundExtensionType Vector3 = new(0x02, typeof(float), 3);
    public static readonly CompoundExtensionType Vector4 = new(0x03, typeof(float), 4);
    public static readonly CompoundExtensionType Vector2Int = new(0x04, typeof(int), 2);
    public static readonly CompoundExtensionType Vector3Int = new(0x05, typeof(int), 3);
    public static readonly CompoundExtensionType Quaternion = new(0x07, typeof(float), 4);
    public static readonly CompoundExtensionType Rect = new(0x08, typeof(float), 4);
    public static readonly CompoundExtensionType Bounds = new(0x09, typeof(float), 6);
    public static readonly CompoundExtensionType RectInt = new(0x0A, typeof(int), 4);
    public static readonly CompoundExtensionType BoundsInt = new(0x0B, typeof(int), 6);

    // Static values for predefined raw extension types
    public static readonly RawExtensionType Color32 = new(0x06, 4);

    // Static values for predefined lists of extension types
    public static readonly IReadOnlyList<ExtensionType> StandardExtensionTypes = new ExtensionType[] {
      // Compound extension types
      Vector2, Vector3, Vector4, Vector2Int, Vector3Int, Quaternion, Rect, Bounds, RectInt, BoundsInt, 

      // Raw extension types
      Color32,
    };
    #endregion
  }
}