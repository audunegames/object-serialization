namespace Audune.Pickle
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
    public void FromBytes(byte[] bytes, ref T value)
    {
      value = FromBytes(bytes);
    }


    // Convert the specified value to a state
    State ITypeAdapter<T>.ToState(T value)
    {
      return new RawState(extensionType, ToBytes(value));
    }

    // Convert the specified state to a value
    T ITypeAdapter<T>.FromState(State state)
    {
      if (state is not RawState rawState)
        throw new StateTypeException(typeof(RawState), state.GetType());

      if (rawState.extensionType != extensionType)
        throw new StateException($"Expected state with extension type {extensionType}, but got {rawState.extensionType}");

      return FromBytes(rawState.bytes);
    }

    // Convert the specified state into an existing value
    void ITypeAdapter<T>.FromState(State state, ref T value)
    {
      if (state is not RawState rawState)
        throw new StateTypeException(typeof(RawState), state.GetType());

      if (rawState.extensionType != extensionType)
        throw new StateException($"Expected state with extension type {extensionType}, but got {rawState.extensionType}");

      FromBytes(rawState.bytes, ref value);
    }
  }
}