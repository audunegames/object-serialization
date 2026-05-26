using System;
using UnityEngine;

namespace Audune.Serialization
{
  // Class that defines an abstract state
  public abstract class State
  {
    #region Returning states
    // Return the state as a value state
    public virtual IValueState AsValue()
    {
      throw new StateTypeException(typeof(IValueState), GetType());
    }

    // Return the state as a list state
    public virtual IListState AsList()
    {
      throw new StateTypeException(typeof(IListState), GetType());
    }

    // Return the state as an object state
    public virtual IObjectState AsObject()
    {
      throw new StateTypeException(typeof(IObjectState), GetType());
    }
    #endregion

    #region Checking for states
    // Return if the state is a value state with the specified type and store the state
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

    // Return if the state is a value state
    public bool IsValue()
    {
      return IsValue(out _);
    }

    // Return if the state is a list state and store the state
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

    // Return if the state is a list state
    public bool IsList()
    {
      return IsList(out _);
    }

    // Return if the state is an object state and store the state
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

    // Return if the state is an object state
    public bool IsObject()
    {
      return IsObject(out _);
    }
    #endregion

    #region Implicit operators
    // Operators that convert from bool values to states
    public static implicit operator State(bool value) => new ValueState(value);
    
    // Operators that convert from numeric values to states
    public static implicit operator State(byte value) => new ValueState(value);
    public static implicit operator State(sbyte value) => new ValueState(value);
    public static implicit operator State(ushort value) => new ValueState(value);
    public static implicit operator State(short value) => new ValueState(value);
    public static implicit operator State(uint value) => new ValueState(value);
    public static implicit operator State(int value) => new ValueState(value);
    public static implicit operator State(ulong value) => new ValueState(value);
    public static implicit operator State(long value) => new ValueState(value);
    public static implicit operator State(float value) => new ValueState(value);
    public static implicit operator State(double value) => new ValueState(value);

    // Operators that convert from span values to states
    public static implicit operator State(string value) => new ValueState(value);
    public static implicit operator State(byte[] value) => new ValueState(value);
    
    // Operators that convert from compound values to states
    public static implicit operator State(Vector2 value) => TypeAdapter.Vector2.ToState(value);
    public static implicit operator State(Vector3 value) => TypeAdapter.Vector3.ToState(value);
    public static implicit operator State(Vector4 value) => TypeAdapter.Vector4.ToState(value);
    public static implicit operator State(Vector2Int value) => TypeAdapter.Vector2Int.ToState(value);
    public static implicit operator State(Vector3Int value) => TypeAdapter.Vector3Int.ToState(value);
    public static implicit operator State(Color32 value) => TypeAdapter.Color32.ToState(value);
    public static implicit operator State(Color value) => TypeAdapter.Color.ToState(value);
    public static implicit operator State(Quaternion value) => TypeAdapter.Quaternion.ToState(value);
    public static implicit operator State(Rect value) => TypeAdapter.Rect.ToState(value);
    public static implicit operator State(RectInt value) => TypeAdapter.RectInt.ToState(value);
    public static implicit operator State(Bounds value) => TypeAdapter.Bounds.ToState(value);
    public static implicit operator State(BoundsInt value) => TypeAdapter.BoundsInt.ToState(value);
    public static implicit operator State(Guid value) => TypeAdapter.Guid.ToState(value);


    // Operators that convert from states to bool values
    public static implicit operator bool(State state) => state.AsValue().Get<bool>();

    // Operators that convert from states to numeric values
    public static implicit operator byte(State state) => state.AsValue().Get<byte>();
    public static implicit operator sbyte(State state) => state.AsValue().Get<sbyte>();
    public static implicit operator ushort(State state) => state.AsValue().Get<ushort>();
    public static implicit operator short(State state) => state.AsValue().Get<short>();
    public static implicit operator uint(State state) => state.AsValue().Get<uint>();
    public static implicit operator int(State state) => state.AsValue().Get<int>();
    public static implicit operator ulong(State state) => state.AsValue().Get<ulong>();
    public static implicit operator long(State state) => state.AsValue().Get<long>();
    public static implicit operator float(State state) => state.AsValue().Get<float>();
    public static implicit operator double(State state) => state.AsValue().Get<double>();

    // Operators that convert from states to span values
    public static implicit operator string(State state) => state.AsValue().Get<string>();
    public static implicit operator byte[](State state) => state.AsValue().Get<byte[]>();
    
    // Operators that convert from states to compound values
    public static implicit operator Vector2(State state) => TypeAdapter.Vector2.FromState(state);
    public static implicit operator Vector3(State state) => TypeAdapter.Vector3.FromState(state);
    public static implicit operator Vector4(State state) => TypeAdapter.Vector4.FromState(state);
    public static implicit operator Vector2Int(State state) => TypeAdapter.Vector2Int.FromState(state);
    public static implicit operator Vector3Int(State state) => TypeAdapter.Vector3Int.FromState(state);
    public static implicit operator Color32(State state) => TypeAdapter.Color32.FromState(state);
    public static implicit operator Color(State state) => TypeAdapter.Color.FromState(state);
    public static implicit operator Quaternion(State state) => TypeAdapter.Quaternion.FromState(state);
    public static implicit operator Rect(State state) => TypeAdapter.Rect.FromState(state);
    public static implicit operator RectInt(State state) => TypeAdapter.RectInt.FromState(state);
    public static implicit operator Bounds(State state) => TypeAdapter.Bounds.FromState(state);
    public static implicit operator BoundsInt(State state) => TypeAdapter.BoundsInt.FromState(state);
    public static implicit operator Guid(State state) => TypeAdapter.Guid.FromState(state);
    #endregion
  }
}