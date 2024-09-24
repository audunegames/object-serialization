namespace Audune.Pickle
{
  // Interface that defines a registry for extenstion types
  public interface IExtensionTypeRegistry
  {
    // Register the specified extension type
    public void RegisterExtensionType(ExtensionType type);

    // Unregister the specified extension type
    public void UnregisterExtensionType(ExtensionType type);

    // Return if a extension type for the specified extension code exists and store the extension type
    public bool TryGetExtensionTypeByCode(sbyte code, out ExtensionType type);
  }
}