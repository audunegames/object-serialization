namespace Audune.Serialization
{
  /// <summary>
  /// Interface that defines a state that holds a value.
  /// </summary>
  public interface IValueState
  {
    /// <summary>
    /// Return the value of the state.
    /// </summary>
    public object value { get; }
    
    
    /// <summary>
    /// Return if the state holds a <see langword="null"/> value.
    /// </summary>
    /// <returns>If the state holds a <see langword="null"/> value.</returns>
    public bool IsNull()
    {
      return value == null;
    }
    
    /// <summary>
    /// Return if the value of the state is of the specified type and store the cast value in <paramref name="castValue"/>.
    /// </summary>
    /// <param name="castValue">The cast value.</param>
    /// <typeparam name="TValue">The type of the value to check.</typeparam>
    /// <returns>If the value of the state is of the specified type.</returns>
    public bool TryGet<TValue>(out TValue castValue)
    {
      return value.TryCast(out castValue).IsSuccessful();
    }

    /// <summary>
    /// Return if the value of the state is of the specified type.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to check.</typeparam>
    /// <returns>If the value of the state is of the specified type.</returns>
    public bool TryGet<TValue>()
    {
      return TryGet<TValue>(out _);
    }

    /// <summary>
    /// Return the value of the state cast to the specified type.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to check.</typeparam>
    /// <returns>The value of the state cast to the specified type.</returns>
    /// <exception cref="StateValueException">If the value of the state is not of the specified type.</exception>
    public TValue Get<TValue>()
    {
      if (!TryGet<TValue>(out var castValue))
        throw new StateValueException(typeof(TValue), value.GetType());
      
      return castValue;
    }
  }
}