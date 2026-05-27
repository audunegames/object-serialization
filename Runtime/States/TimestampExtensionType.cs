using MessagePack;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an extension type for timestamps.
  /// </summary>
  public sealed class TimestampExtensionType : ExtensionType
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    public TimestampExtensionType() : base(ReservedMessagePackExtensionTypeCode.DateTime)
    {
    }
  }
}
