using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a type adapter for <see cref="System.Guid"/> objects.
  /// </summary>
  internal class GuidTypeAdapter : IRawTypeAdapter<Guid>
  {
    /// <inheritdoc/>
    public RawExtensionType extensionType => ExtensionType.Guid;

    
    /// <inheritdoc/>
    public byte[] ToBytes(Guid value)
    {
      return value.ToByteArray();
    }

    /// <inheritdoc/>
    public Guid FromBytes(byte[] bytes)
    {
      return new Guid(bytes);
    }
  }
}