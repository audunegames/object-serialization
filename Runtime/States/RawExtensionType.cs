namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an extension type for a <see cref="RawExtensionState"/>.
  /// </summary>
  public class RawExtensionType : ExtensionType
  {
    /// <summary>
    /// The length of the raw extension type,
    /// </summary>
    internal readonly uint length;

    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="code">The code of the extension type.</param>
    /// <param name="length">The length of the extension type.</param>
    public RawExtensionType(sbyte code, uint length) : base(code)
    {
      this.length = length;
    }
  }
}