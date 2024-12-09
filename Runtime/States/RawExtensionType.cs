namespace Audune.Serialization
{
  // Class that defines an extension type that matches a raw extension state
  public class RawExtensionType : ExtensionType
  {
    // The length of the raw extension type
    internal readonly uint length;


    // Constructor
    public RawExtensionType(sbyte code, uint length) : base(code)
    {
      this.length = length;
    }
  }
}