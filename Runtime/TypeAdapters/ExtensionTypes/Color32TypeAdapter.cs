using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="UnityEngine.Color32"/> objects.
  /// </summary>
  internal class Color32TypeAdapter : IRawTypeAdapter<Color32>, IRawTypeAdapter<Color>
  {
    /// <inheritdoc/>
    public RawExtensionType extensionType => ExtensionType.Color32;
    
    /// <inheritdoc/>
    RawExtensionType IRawTypeAdapter<Color>.extensionType => ExtensionType.Color32;

    
    /// <inheritdoc/>
    public byte[] ToBytes(Color32 value)
    {
      var bytes = new[] { value.r, value.g, value.b, value.a };
      return bytes;
    }

    /// <inheritdoc/>
    public Color32 FromBytes(byte[] bytes)
    {
      return new Color32(bytes[0], bytes[1], bytes[2], bytes[3]);
    }


    /// <inheritdoc/>
    byte[] IRawTypeAdapter<Color>.ToBytes(Color value)
    {
      return ToBytes(value);
    }

    /// <inheritdoc/>
    Color IRawTypeAdapter<Color>.FromBytes(byte[] bytes)
    {
      return FromBytes(bytes);
    }
  }
}