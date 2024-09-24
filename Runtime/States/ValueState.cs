using System;

namespace Audune.Pickle
{
  // Class that defines a value state
  public class ValueState : State, IEquatable<ValueState>
  {
    // The value of the value state
    private readonly object _value;


    // Return the value of the value state
    internal object value => _value;


    // Constructor
    internal ValueState(object value)
    {
      _value = value;
    }


    #region Returning states
    // Return the state as a value state
    public override ValueState AsValue()
    {
      return this;
    }
    #endregion

    #region Returning values
    // Return if the value of the state is of the specified type and store the value
    public bool TryGet<TValue>(out TValue castValue)
    {
      return _value.TryCast(out castValue).IsSuccesful();
    }

    // Return if the value of the state is of the specified type
    public bool TryGet<TValue>()
    {
      return TryGet<TValue>(out _);
    }

    // Return the value of the state as the specified type
    public TValue Get<TValue>()
    {
      if (!TryGet<TValue>(out var castValue))
        throw new StateValueException(typeof(TValue), _value.GetType());
      
      return castValue;
    }
    #endregion

    #region Equatable implementation
    // Return if the state equals another object
    public override bool Equals(object other)
    {
      return Equals(other as ValueState);
    }

    // Return if the state equals another state
    public bool Equals(ValueState other)
    {
      return other is not null
        && Equals(_value, other._value);
    }

    // Return the hash code of the state
    public override int GetHashCode()
    {
      return HashCode.Combine(_value);
    }


    // Return if the state equals another state using the equal operator
    public static bool operator ==(ValueState left, ValueState right)
    {
      return Equals(left, right);
    }
    
    // Return if the state does not equal another state using the not equal operator
    public static bool operator !=(ValueState left, ValueState right)
    {
      return !(left == right);
    }
    #endregion

    #region Implicit operators
    // Operators that convert from states to bool values
    public static implicit operator bool(ValueState state) => state.Get<bool>();

    // Operators that convert from states to numeric values
    public static implicit operator byte(ValueState state) => state.Get<byte>();
    public static implicit operator sbyte(ValueState state) => state.Get<sbyte>();
    public static implicit operator ushort(ValueState state) => state.Get<ushort>();
    public static implicit operator short(ValueState state) => state.Get<short>();
    public static implicit operator uint(ValueState state) => state.Get<uint>();
    public static implicit operator int(ValueState state) => state.Get<int>();
    public static implicit operator ulong(ValueState state) => state.Get<ulong>();
    public static implicit operator long(ValueState state) => state.Get<long>();
    public static implicit operator float(ValueState state) => state.Get<float>();
    public static implicit operator double(ValueState state) => state.Get<double>();

    // Operators that convert from states to span values
    public static implicit operator string(ValueState state) => state.Get<string>();
    public static implicit operator byte[](ValueState state) => state.Get<byte[]>();
    #endregion
  }
}