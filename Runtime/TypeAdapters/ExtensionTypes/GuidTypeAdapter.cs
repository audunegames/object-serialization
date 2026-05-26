using System;

namespace Audune.Serialization
{
  // Class that defines a type adapter for GUID objects
  internal class GuidTypeAdapter : IRawTypeAdapter<Guid>
  {
    // The extension type of the type adapter
    public RawExtensionType extensionType => ExtensionType.Guid;

    
    // Convert the specified value to a byte array
    public byte[] ToBytes(Guid value)
    {
      return value.ToByteArray();
    }

    // Convert the specified byte array to a value
    public Guid FromBytes(byte[] bytes)
    {
      return new Guid(bytes);
    }
  }
}