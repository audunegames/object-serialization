using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines predefined type adapters.
  /// </summary>
  public static class TypeAdapter
  {
    /// <summary>
    /// Type adapter for <see cref="System.Boolean"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<bool> Bool = new ValueTypeAdapter<bool>();

    /// <summary>
    /// Type adapter for <see cref="System.Byte"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<byte> UInt8 = new ValueTypeAdapter<byte>();
    
    /// <summary>
    /// Type adapter for <see cref="System.SByte"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<sbyte> Int8 = new ValueTypeAdapter<sbyte>();
    
    /// <summary>
    /// Type adapter for <see cref="System.UInt16"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<ushort> UInt16 = new ValueTypeAdapter<ushort>();
    
    /// <summary>
    /// Type adapter for <see cref="System.Int16"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<short> Int16 = new ValueTypeAdapter<short>();
    
    /// <summary>
    /// Type adapter for <see cref="System.UInt32"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<uint> UInt32 = new ValueTypeAdapter<uint>();
    
    /// <summary>
    /// Type adapter for <see cref="System.Int32"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<int> Int32 = new ValueTypeAdapter<int>();
    
    /// <summary>
    /// Type adapter for <see cref="System.UInt64"/>objects.
    /// </summary>
    public static readonly ITypeAdapter<ulong> UInt64 = new ValueTypeAdapter<ulong>();
    
    /// <summary>
    /// Type adapter for <see cref="System.Int64"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<long> Int64 = new ValueTypeAdapter<long>();
    
    /// <summary>
    /// Type adapter for <see cref="System.Single"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<float> Single = new ValueTypeAdapter<float>();
    
    /// <summary>
    /// Type adapter for <see cref="System.Double"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<double> Double = new ValueTypeAdapter<double>();

    /// <summary>
    /// Type adapter for <see cref="System.String"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<string> String = new ValueTypeAdapter<string>();
    
    /// <summary>
    /// Type adapter for <see cref="System.Byte"/>[] objects.
    /// </summary>
    public static readonly ITypeAdapter<byte[]> Binary = new ValueTypeAdapter<byte[]>();
    
    /// <summary>
    /// Type adapter for <see cref="System.DateTime"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<DateTime> Timestamp = new ValueTypeAdapter<DateTime>();

    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Vector2"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Vector2> Vector2 = new Vector2TypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Vector3"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Vector3> Vector3 = new Vector3TypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Vector4"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Vector4> Vector4 = new Vector4TypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Vector2Int"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Vector2Int> Vector2Int = new Vector2IntTypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Vector3Int"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Vector3Int> Vector3Int = new Vector3IntTypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Color32"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Color32> Color32 = new Color32TypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Color"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Color> Color = new Color32TypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Quaternion"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Quaternion> Quaternion = new QuaternionTypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Rect"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Rect> Rect = new RectTypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.RectInt"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<RectInt> RectInt = new RectIntTypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.Bounds"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Bounds> Bounds = new BoundsTypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="UnityEngine.BoundsInt"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<BoundsInt> BoundsInt = new BoundsIntTypeAdapter();
    
    /// <summary>
    /// Type adapter for <see cref="System.Guid"/> objects.
    /// </summary>
    public static readonly ITypeAdapter<Guid> Guid = new GuidTypeAdapter();
    
    
    
    /// <summary>
    /// Dictionary that defines the standard type adapters for a serializer.
    /// </summary>
    public static readonly Dictionary<Type, object> StandardTypeAdapters = new Dictionary<Type, object>()
    {
      { typeof(bool), Bool },
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
      { typeof(string), String },
      { typeof(byte[]), Binary },
      { typeof(DateTime), Timestamp },
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
      { typeof(Guid), Guid },
    };
  }
}