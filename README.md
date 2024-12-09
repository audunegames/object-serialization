# Audune Object Serialization

[![openupm](https://img.shields.io/npm/v/com.audune.serialization?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.audune.serialization/)

Custom and extensible object serialization for Unity, using intermediate state classes.

See the [wiki](https://github.com/audunegames/object-serialization/wiki) of the repository to get started with the package.

## Features

* Serialize and deserialize primtive values and Unity structs to and from intermediate state classes respectively. Easily create your own serializable classes and structs by implementing the `ISerializable` interface and crafting a state structure reminiscent of JSON.
* Encode and decode states and objects that can be serialized to states to a string or byte array for storage on a filesystem or sending over a network.
* Use the [Object Persistence package](https://github.com/audunegames/object-persistence) for a higher-level library to save and load states to and from the filesystem, repsectively, or any other filesystem adapter.

### Supported serialization types

* C# value types: `bool`, `byte`, `sbyte`, `ushort`, `short`, `uint`, `int`, `ulong`, `long`, `float`, `double`.
* C# reference types: `string`, `byte[]`.
* Unity structs: `Vector2`, `Vector3`, `Vector4`, `Vector2Int`, `Vector3Int`, `Color32`, `Color`, `Quaternion`, `Rect`, `Bounds`, `RectInt`, `BoundsInt`.
* Any custom class that implements `Audune.Serialization.ISerializable`.

### Supported encoders

* [MessagePack](https://msgpack.org/) with optional LZ4 compression, using the [MessagePack-CSharp](https://github.com/MessagePack-CSharp/MessagePack-CSharp) library.

## Installation

### Requirements

This package depends on the following packages:

* [MessagePack](https://openupm.com/packages/net.tnrd.messagepack/), version **2.5.125** or higher.

If you're installing the required packages from the [OpenUPM registry](https://openupm.com/), make sure to add a scoped registry with the URL `https://package.openupm.com` and the required scopes before installing the packages.

### Installing from the OpenUPM registry

To install this package as a package from the OpenUPM registry in the Unity Editor, use the following steps:

* In the Unity editor, navigate to **Edit › Project Settings... › Package Manager**.
* Add the following Scoped Registry, or edit the existing OpenUPM entry to include the new Scope:

```
Name:     package.openupm.com
URL:      https://package.openupm.com
Scope(s): com.audune.serialization
```

* Navigate to **Window › Package Manager**.
* Click the **+** icon and click **Add package by name...**
* Enter the following name in the corresponding field and click **Add**:

```
com.audune.serialization
```

### Installing as a Git package

To install this package as a Git package in the Unity Editor, use the following steps:

* In the Unity editor, navigate to **Window › Package Manager**.
* Click the **+** icon and click **Add package from git URL...**
* Enter the following URL in the URL field and click **Add**:

```
https://github.com/audunegames/object-serialization.git
```

## License

This package is licensed under the GNU GPL 3.0 license. See `LICENSE.txt` for more information.
