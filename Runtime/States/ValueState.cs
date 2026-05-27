using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a state that holds a value.
  /// </summary>
  public class ValueState : State, IValueState, IEquatable<ValueState>
  {
    /// <summary>
    /// The value of the state.
    /// </summary>
    private readonly object _value;


    /// <ineritdoc/>
    public object value => _value;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">The value of the value state.</param>
    internal ValueState(object value)
    {
      _value = value;
    }


    #region Returning and converting states
    /// <inheritdoc/>
    public override bool IsNull()
    {
      return value == null;
    }
    
    /// <inheritdoc/>
    bool IValueState.IsNull()
    {
      return IsNull();
    }
    
    /// <inheritdoc/>
    public override IValueState AsValue()
    {
      return this;
    }
    #endregion

    #region Value state implementation
    /// <ineritdoc/>
    public bool TryGet<TValue>(out TValue castValue)
    {
      return value.TryCast(out castValue).IsSuccessful();
    }

    /// <ineritdoc/>
    public bool TryGet<TValue>()
    {
      return TryGet<TValue>(out _);
    }

    /// <ineritdoc/>
    public TValue Get<TValue>()
    {
      if (!TryGet<TValue>(out var castValue))
        throw new StateValueException(typeof(TValue), value.GetType());
      
      return castValue;
    }
    #endregion

    #region Equatable implementation
    /// <ineritdoc/>
    public override bool Equals(object other)
    {
      return Equals(other as ValueState);
    }

    /// <ineritdoc/>
    public bool Equals(ValueState other)
    {
      return other is not null
        && Equals(_value, other._value);
    }

    /// <ineritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(_value);
    }


    /// <summary>
    /// Return if the specified <see cref="ValueState"/>s are equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="ValueState"/> to compare.</param>
    /// <param name="right">The right <see cref="ValueState"/> to compare.</param>
    /// <returns>If the specified <see cref="ValueState"/>s are equal.</returns>
    public static bool operator ==(ValueState left, ValueState right)
    {
      return Equals(left, right);
    }
    
    /// <summary>
    /// Return if the specified <see cref="ValueState"/>s are not equal to each other.
    /// </summary>
    /// <param name="left">The left <see cref="ValueState"/> to compare.</param>
    /// <param name="right">The right <see cref="ValueState"/> to compare.</param>
    /// <returns>If the specified <see cref="ValueState"/>s are equal.</returns>
    public static bool operator !=(ValueState left, ValueState right)
    {
      return !(left == right);
    }
    #endregion

    #region Implicit operators
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.Boolean"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator bool(ValueState state) => state.Get<bool>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.Byte"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator byte(ValueState state) => state.Get<byte>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.SByte"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator sbyte(ValueState state) => state.Get<sbyte>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.UInt16"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator ushort(ValueState state) => state.Get<ushort>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.Int16"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator short(ValueState state) => state.Get<short>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.UInt32"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator uint(ValueState state) => state.Get<uint>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.Int32"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator int(ValueState state) => state.Get<int>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.UInt64"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator ulong(ValueState state) => state.Get<ulong>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.Int64"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator long(ValueState state) => state.Get<long>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.Single"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator float(ValueState state) => state.Get<float>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.Double"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator double(ValueState state) => state.Get<double>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.String"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator string(ValueState state) => state.Get<string>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.Byte"/>[] value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator byte[](ValueState state) => state.Get<byte[]>();
    
    /// <summary>
    /// Convert a <see cref="ValueState"/> to a <see cref="System.String"/> value.
    /// </summary>
    /// <param name="state">The state to convert.</param>
    /// <returns>The converted value.</returns>
    public static implicit operator DateTime(ValueState state) => state.Get<DateTime>();
    #endregion
  }
}