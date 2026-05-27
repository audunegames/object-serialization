using System.Collections.Generic;

namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines a registry for extenstion types.
  /// </summary>
  public interface IExtensionTypeRegistry
  {
    /// <summary>
    /// Register the specified extension type.
    /// </summary>
    /// <param name="type">The extension type to register.</param>
    public void RegisterExtensionType(ExtensionType type);

    /// <summary>
    /// Register all specified extension types.
    /// </summary>
    /// <param name="extensionTypes">An enumerable of extension types to register.</param>
    public void RegisterExtensionTypes(IEnumerable<ExtensionType> extensionTypes);

    /// <summary>
    /// Unregister the specified extension type.
    /// </summary>
    /// <param name="type">The extension type to unregister.</param>
    public void UnregisterExtensionType(ExtensionType type);

    /// <summary>
    /// Unregister all specified extension types.
    /// </summary>
    /// <param name="extensionTypes">An enumerable of extension types to unregister.</param>
    public void UnregisterExtensionTypes(IEnumerable<ExtensionType> extensionTypes);

    /// <summary>
    /// Return if an extension type for the specified code exists and store the extension type in <paramref name="type"/>.
    /// </summary>
    /// <param name="code">The code of the extension type.</param>
    /// <param name="type">The extension type that matches the code.</param>
    /// <returns>If an extension type for the specified code exists.</returns>
    public bool TryGetExtensionTypeByCode(sbyte code, out ExtensionType type);
  }
}