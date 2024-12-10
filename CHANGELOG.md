# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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