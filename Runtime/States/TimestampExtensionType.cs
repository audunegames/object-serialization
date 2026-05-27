using MessagePack;

namespace Audune.Serialization
{
  // Class that defines a timestamp extension type
  public sealed class TimestampExtensionType : ExtensionType
  {
    // Constructor
    public TimestampExtensionType() : base(ReservedMessagePackExtensionTypeCode.DateTime)
    {
    }
  }
}
