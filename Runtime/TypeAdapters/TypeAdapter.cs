using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audune.Pickle
{
  // Class that defines predefined type adapters
  public static class TypeAdapter
  {
    // Static values for predefined bool value type adapters
    public static readonly ITypeAdapter<bool> Bool = new ValueTypeAdapter<bool>();

    // Static values for predefined numeric value type adapters
    public static readonly ITypeAdapter<byte> UInt8 = new ValueTypeAdapter<byte>();
    public static readonly ITypeAdapter<sbyte> Int8 = new ValueTypeAdapter<sbyte>();
    public static readonly ITypeAdapter<ushort> UInt16 = new ValueTypeAdapter<ushort>();
    public static readonly ITypeAdapter<short> Int16 = new ValueTypeAdapter<short>();
    public static readonly ITypeAdapter<uint> UInt32 = new ValueTypeAdapter<uint>();
    public static readonly ITypeAdapter<int> Int32 = new ValueTypeAdapter<int>();
    public static readonly ITypeAdapter<ulong> UInt64 = new ValueTypeAdapter<ulong>();
    public static readonly ITypeAdapter<long> Int64 = new ValueTypeAdapter<long>();
    public static readonly ITypeAdapter<float> Single = new ValueTypeAdapter<float>();
    public static readonly ITypeAdapter<double> Double = new ValueTypeAdapter<double>();

    // Static values for predefined span value type adapters
    public static readonly ITypeAdapter<string> String = new ValueTypeAdapter<string>();
    public static readonly ITypeAdapter<byte[]> Binary = new ValueTypeAdapter<byte[]>();

    // Static values for predefined compound value type adapters
    public static readonly ITypeAdapter<Vector2> Vector2 = new Vector2TypeAdapter();
    public static readonly ITypeAdapter<Vector3> Vector3 = new Vector3TypeAdapter();
    public static readonly ITypeAdapter<Vector4> Vector4 = new Vector4TypeAdapter();
    public static readonly ITypeAdapter<Vector2Int> Vector2Int = new Vector2IntTypeAdapter();
    public static readonly ITypeAdapter<Vector3Int> Vector3Int = new Vector3IntTypeAdapter();
    public static readonly ITypeAdapter<Color32> Color32 = new Color32TypeAdapter();
    public static readonly ITypeAdapter<Color> Color = new Color32TypeAdapter();
    public static readonly ITypeAdapter<Quaternion> Quaternion = new QuaternionTypeAdapter();
    public static readonly ITypeAdapter<Rect> Rect = new RectTypeAdapter();
    public static readonly ITypeAdapter<RectInt> RectInt = new RectIntTypeAdapter();
    public static readonly ITypeAdapter<Bounds> Bounds = new BoundsTypeAdapter();
    public static readonly ITypeAdapter<BoundsInt> BoundsInt = new BoundsIntTypeAdapter();

    // Static values for predefined dictionaries of compound types
    public static readonly Dictionary<Type, object> StandardTypeAdapters = new Dictionary<Type, object>()
    {
      // Bool value type adapters
      { typeof(bool), Bool },

      // Numeric value type adapters
      { typeof(byte), UInt8 },
      { typeof(sbyte), Int8 },
      { typeof(ushort), UInt16 },
      { typeof(short), Int16 },
      { typeof(uint), UInt32 },
      { typeof(int), Int32 },
      { typeof(ulong), UInt64 },
      { typeof(long), Int64 },
      { typeof(float), Single },
      { typeof(double), Double },

      // Span value type adapters
      { typeof(string), String },
      { typeof(byte[]), Binary },

      // Compound value type adapters
      { typeof(Vector2), Vector2 },
      { typeof(Vector3), Vector3 },
      { typeof(Vector4), Vector4 },
      { typeof(Vector2Int), Vector2Int },
      { typeof(Vector3Int), Vector3Int },
      { typeof(Color32), Color32 },
      { typeof(Color), Color },
      { typeof(Quaternion), Quaternion },
      { typeof(Rect), Rect },
      { typeof(RectInt), RectInt },
      { typeof(Bounds), Bounds },
      { typeof(BoundsInt), BoundsInt },
    };
  }
}