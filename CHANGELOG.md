# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.1] - 2025-05-29

### Fixed

- Fixed null states throwing an exception when being serialized instead of being encoded correctly.
- Fixed null states not being read while deserializing.

## [1.1.0] - 2026-05-27

### Added

- Added documentation comments for all scripts.
- Added support for serializing `System.DateTime` through the timestamp extension type.
- Added support for serializing `System.Guid` with a raw exension state.
- Added `IsNull` method to the state class to check if its value is null.
- Added methods to register and unregister an enumerable of extension types on `IExtensionTypeRegisty`.
- Implemented `IReadOnlyList<State>` for `IListState` and `IReadOnlyDictionary<string, State>` for `IObjectState`.

### Changed

- Allow private parameterless constructors when deserializing states in the serializer.

## [1.0.7] - 2026-05-26

### Changed

- Made deserialization extension methods more generic by using `ICollection<T>` as parameter.

## [1.0.6] - 2026-05-26

### Added

- Added implicit operators to convert states to compound values using type adapters and vice versa.
- Added extension methods to convert from enumerables to list and object states and vice versa.

### Deprecated

- Made `SerializeWithKey` and `DeserializeWithKey` extension methods obsolete in favor of `SerializeToObject` and `ApplyDeserializationFromObject`.

### Fixed

- Fixed recursive call in `ListState.Get<T>`.
- Fixed recursive call in `RawExtensionState.Equals` due to wrong type name.

## [1.0.5] - 2026-05-18

### Security

- Added sign and release workflow to sign and release a UPM package when a Git tag is pushed.

## [1.0.4] - 2025-09-08

### Added

- Added support for serializing null value states.

### Changed

- Modified serializable extension methods to be extensions on `ISerializationContext` and `IDeserializationContext`, and made the methods generic to be able to work with any enumerable.

## [1.0.3] - 2025-09-06

### Changed

- Modified serializable extension methods to deal with enumerables instead of dictionaries.

## [1.0.2] - 2024-12-27

### Changed

- Updated source code license from GPL 3.0 to LGPL 3.0, so that larger works can use the library without disclosing their source code.

## [1.0.1] - 2024-12-10

### Added

- Generic versions of serializable and deserializable interfaces.
- Generic methods to get states from lists and objects

### Fixed

- Fixed stack overflow bug in object state class.

## [1.0.0] - 2024-12-10

### Added

- Serialize and deserialize primtive values and Unity structs to and from intermediate state classes respectively. 
- Create custom serializable classes and structs by implementing the ISerializable interface and crafting a state structure reminiscent of JSON.
- Encode and decode states and objects that can be serialized to states to a string or byte array for storage on a filesystem or sending over a network.