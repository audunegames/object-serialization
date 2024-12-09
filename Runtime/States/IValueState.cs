namespace Audune.Serialization
{
  // Interface that defines a value state
  public interface IValueState
  {
    // Return the value of the value state
    public object value { get; }


    // Return if the value of the state is of the specified type and store the value
    public bool TryGet<TValue>(out TValue castValue)
    {
      return value.TryCast(out castValue).IsSuccesful();
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
        throw new StateValueException(typeof(TValue), value.GetType());
      
      return castValue;
    }
  }
}