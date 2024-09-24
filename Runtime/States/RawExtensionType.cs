namespace Audune.Pickle
{
  // Class that defines an extension type that matches a raw value state
  public class RawExtensionType : ExtensionType
  {
    // The length of the raw extension type
    private readonly uint _length;


    // Return the length of the raw extension type
    internal uint length => _length;


    // Constructor
    public RawExtensionType(sbyte code, uint length) : base(code)
    {
      _length = length;
    }
  }
}