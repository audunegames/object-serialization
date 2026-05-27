using System;
using UnityEngine;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines an abstract state.
  /// </summary>
  public abstract class State
  {
    #region Returning and converting states
    /// <summary>
    /// Return if the state holds a <see langword="null"/> value.
    /// </summary>
    /// <returns>If the state holds a <see langword="null"/> value.</returns>
    public virtual bool IsNull()
    {
      return false;
    }
    
    
    /// <summary>
    /// Return the state as an <see cref="IValueState"/>.
    /// </summary>
    /// <returns>The state as an <see cref="IValueState"/>.</returns>
    /// <exception cref="StateTypeException">If the state is not an <see cref="IValueState"/>.</exception>
    public virtual IValueState AsValue()
    {
      throw new StateTypeException(typeof(IValueState), GetType());
    }
    
    /// <summary>
    /// Return if the state is an <see cref="IValueState"/> and store the state in <paramref name="valueState"/>.
    /// </summary>
    /// <param name="valueState">The state cast to an <see cref="IValueState"/>.</param>
    /// <returns>If the state is an <see cref="IValueState"/>.</returns>
    public bool IsValue(out IValueState valueState)
    {
      try
      {
        valueState = AsValue();
        return true;
      }
      catch (StateException)
      {
        valueState = null;
        return false;
      }
    }

    /// <summary>
    /// Return if the state is an <see cref="IValueState"/>.
    /// </summary>
    /// <returns>If the state is an <see cref="IValueState"/>.</returns>
    public bool IsValue()
    {
      return IsValue(out _);
    }
    

    /// <summary>
    /// Return the state as an <see cref="IListState"/>.
    /// </summary>
    /// <returns>The state as an <see cref="IListState"/>.</returns>
    /// <exception cref="StateTypeException">If the state is not an <see cref="IListState"/>.</exception>
    public virtual IListState AsList()
    {
      throw new StateTypeException(typeof(IListState), GetType());
    }
    
    /// <summary>
    /// Return if the state is an <see cref="IListState"/> and store the state in <paramref name="listState"/>.
    /// </summary>
    /// <param name="listState">The state cast to an <see cref="IListState"/>.</param>
    /// <returns>If the state is an <see cref="IListState"/>.</returns>
    public bool IsList(out IListState listState)
    {
      try
      {
        listState = AsList();
        return true;
      }
      catch (StateException)
      {
        listState = null;
        return false;
      }
    }

    /// <summary>
    /// Return if the state is an <see cref="IListState"/>.
    /// </summary>
    /// <returns>If the state is an <see cref="IListState"/>.</returns>
    public bool IsList()
    {
      return IsList(out _);
    }
    

    /// <summary>
    /// Return the state as an <see cref="IObjectState"/>.
    /// </summary>
    /// <returns>The state as an <see cref="IObjectState"/>.</returns>
    /// <exception cref="StateTypeException">If the state is not an <see cref="IObjectState"/>.</exception>
    public virtual IObjectState AsObject()
    {
      throw new StateTypeException(typeof(IObjectState), GetType());
    }
    
    /// <summary>
    /// Return if the state is an <see cref="IObjectState"/> and store the state in <paramref name="objectState"/>.
    /// </summary>
    /// <param name="objectState">The state cast to an <see cref="IObjectState"/>.</param>
    /// <returns>If the state is an <see cref="IObjectState"/>.</returns>
    public bool IsObject(out IObjectState objectState)
    {
      try
      {
        objectState = AsObject();
        return true;
      }
      catch (StateException)
      {
        objectState = null;
        return false;
      }
    }

    /// <summary>
    /// Return if the state is an <see cref="IObjectState"/>.
    /// </summary>
    /// <returns>If the state is an <see cref="IObjectState"/>.</returns>
    public bool IsObject()
    {
      return IsObject(out _);
    }
    #endregion

    #region Implicit operators
    /// <summary>
    /// Convert a <see cref="System.Boolean"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(bool value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.Byte"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(byte value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.SByte"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(sbyte value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.UInt16"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(ushort value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.Int16"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(short value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.UInt32"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(uint value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.Int32"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(int value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.UInt64"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(ulong value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.Int64"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(long value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.Single"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(float value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.Double"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(double value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.String"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(string value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.Byte"/>[] value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(byte[] value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="System.DateTime"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(DateTime value) => new ValueState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Vector2"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Vector2 value) => TypeAdapter.Vector2.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Vector3"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Vector3 value) => TypeAdapter.Vector3.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Vector4"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Vector4 value) => TypeAdapter.Vector4.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Vector2Int"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Vector2Int value) => TypeAdapter.Vector2Int.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Vector3Int"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Vector3Int value) => TypeAdapter.Vector3Int.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Color32"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Color32 value) => TypeAdapter.Color32.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Color"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Color value) => TypeAdapter.Color.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Quaternion"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Quaternion value) => TypeAdapter.Quaternion.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Rect"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Rect value) => TypeAdapter.Rect.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.RectInt"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(RectInt value) => TypeAdapter.RectInt.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.Bounds"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Bounds value) => TypeAdapter.Bounds.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="UnityEngine.BoundsInt"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(BoundsInt value) => TypeAdapter.BoundsInt.ToState(value);
    
    /// <summary>
    /// Convert a <see cref="System.Guid"/> value to a <see cref="State"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted state.</returns>
    public static implicit operator State(Guid value) => TypeAdapter.Guid.ToState(value);


    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Boolean"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator bool(State state) => state.AsValue().Get<bool>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Byte"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator byte(State state) => state.AsValue().Get<byte>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.SByte"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator sbyte(State state) => state.AsValue().Get<sbyte>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.UInt16"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator ushort(State state) => state.AsValue().Get<ushort>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Int16"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator short(State state) => state.AsValue().Get<short>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.UInt32"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator uint(State state) => state.AsValue().Get<uint>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Int32"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator int(State state) => state.AsValue().Get<int>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.UInt64"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator ulong(State state) => state.AsValue().Get<ulong>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Int64"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator long(State state) => state.AsValue().Get<long>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Single"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator float(State state) => state.AsValue().Get<float>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Double"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator double(State state) => state.AsValue().Get<double>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.String"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator string(State state) => state.AsValue().Get<string>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Byte"/>[] value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator byte[](State state) => state.AsValue().Get<byte[]>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.DateTime"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator DateTime(State state) => state.AsValue().Get<DateTime>();
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Vector2"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Vector2(State state) => TypeAdapter.Vector2.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Vector3"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Vector3(State state) => TypeAdapter.Vector3.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Vector4"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Vector4(State state) => TypeAdapter.Vector4.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Vector2Int"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Vector2Int(State state) => TypeAdapter.Vector2Int.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Vector3Int"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Vector3Int(State state) => TypeAdapter.Vector3Int.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Color32"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Color32(State state) => TypeAdapter.Color32.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Color"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Color(State state) => TypeAdapter.Color.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Quaternion"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Quaternion(State state) => TypeAdapter.Quaternion.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Rect"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Rect(State state) => TypeAdapter.Rect.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.RectInt"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator RectInt(State state) => TypeAdapter.RectInt.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.Bounds"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Bounds(State state) => TypeAdapter.Bounds.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="UnityEngine.BoundsInt"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator BoundsInt(State state) => TypeAdapter.BoundsInt.FromState(state);
    
    /// <summary>
    /// Convert a <see cref="State"/> to a <see cref="System.Guid"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator Guid(State state) => TypeAdapter.Guid.FromState(state);
    #endregion
  }
}