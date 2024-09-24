using UnityEngine;

namespace Audune.Pickle
{
  // Class that defines a type adapter for Color32 objects
  internal class Color32TypeAdapter : IRawTypeAdapter<Color32>, IRawTypeAdapter<Color>
  {
    // The extension type of the type adapter
    public RawExtensionType extensionType => ExtensionType.Color32;

    
    // Convert the specified value to a byte array
    public byte[] ToBytes(Color32 value)
    {
      var bytes = new byte[] { value.r, value.g, value.b, value.a };
      return bytes;
    }

    // Convert the specified byte array to a value
    public Color32 FromBytes(byte[] bytes)
    {
      return new Color32(bytes[0], bytes[1], bytes[2], bytes[3]);
    }


    // Convert the specified color value to a byte array
    byte[] IRawTypeAdapter<Color>.ToBytes(Color value)
    {
      return ToBytes((Color32)value);
    }

    // Convert the specified byte array to a color value
    Color IRawTypeAdapter<Color>.FromBytes(byte[] bytes)
    {
      return (Color)FromBytes(bytes);
    }
  }
}