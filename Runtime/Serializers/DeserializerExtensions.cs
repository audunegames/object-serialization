namespace Audune.Pickle
{
  // Class that defines extension methods for deserializers
  public static class DeserializerExtensions
  {
    // Assert that the specified state is of the specified type
    public static void AssertType<TState>(this State state, out TState assertedState) where TState : State
    {
      if (state is not TState tState)
        throw new DeserializingException($"Expected state of type {typeof(TState)}, but got {state.GetType()}");
      assertedState = tState;
    }

    // Assert that the specified state is of the specified compound type
    public static void AssertCompoundType(this State state, ExtensionType type, out CompoundState assertedState)
    {
      state.AssertType(out assertedState);
      if (assertedState.type != type)
        throw new DeserializingException($"Expected state of type {typeof(CompoundState)} with type {type}, but got type {assertedState.type}");
    }
  }
}