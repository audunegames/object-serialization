namespace Audune.Serialization
{
  // Interface that defines a type adapter for raw extension types
  public interface IRawTypeAdapter<T> : ITypeAdapter<T>
  {    
    // The extension type of the type adapter
    public RawExtensionType extensionType { get; }


    // Convert the specified value to a byte array
    public byte[] ToBytes(T value);

    // Convert the specified byte array to a value
    public T FromBytes(byte[] bytes);

    // Convert the specified byte array into an existing value
    public void FromBytes(byte[] bytes, T value)
    {
      throw new StateException($"Type adapter {GetType()} does not support deserializing states into existing objects");
    }


    // Convert the specified value to a state
    State ITypeAdapter<T>.ToState(T value)
    {
      return new RawExtensionState(extensionType, ToBytes(value));
    }

    // Convert the specified state to a value
    T ITypeAdapter<T>.FromState(State state)
    {
      if (state is not RawExtensionState rawState)
        throw new StateTypeException(typeof(RawExtensionState), state.GetType());

      if (rawState.type != extensionType)
        throw new StateException($"Expected state with extension type {extensionType}, but got {rawState.type}");

      return FromBytes(rawState.bytes);
    }

    // Convert the specified state into an existing value
    void ITypeAdapter<T>.FromState(State state, T value)
    {
      if (state is not RawExtensionState rawState)
        throw new StateTypeException(typeof(RawExtensionState), state.GetType());

      if (rawState.type != extensionType)
        throw new StateException($"Expected state with extension type {extensionType}, but got {rawState.type}");

      FromBytes(rawState.bytes, value);
    }
  }
}