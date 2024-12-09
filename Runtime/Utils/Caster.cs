using System;

namespace Audune.Serialization
{
  // Class that defines extension methods for objects to cast it to a different type
  internal static class Caster
  {
    // Enum that defines the result of a type cast
    public enum Result
    {
      SameTypeCast,
      AssignableTypeCast,
      ConvertableTypeCast,
      InvalidTypeCast,
      OverflowTypeCast,
    }


    // Return how the specified value can be cast to the expected type
    public static Result TryCast(this object value, Type expectedType)
    {
      return TryCastInternal(value, expectedType).result;
    }

    // Return how the specified value can be cast to the expected type and store the cast value
    public static Result TryCast<TObject>(this object value, out TObject castValue)
    {
      var cast = TryCastInternal(value, typeof(TObject));
      castValue = cast.result.IsSuccesful() ? (TObject)cast.castValue : default;
      return cast.result;
    }

    // Return if the result of a type cast was succesful
    public static bool IsSuccesful(this Result result)
    {
      return result == Result.SameTypeCast || result == Result.AssignableTypeCast || result == Result.ConvertableTypeCast;
    }
    

    // Return how the specified value can be cast to the expected type and the cast value
    private static (Result result, object castValue) TryCastInternal(object value, Type expectedType)
    {
      // Check if the type of the value is equal to the expected type
      if (expectedType == value.GetType())
        return (Result.SameTypeCast, value);

      // Check if the type of the value is assignable from the expected type
      if (expectedType.IsAssignableFrom(value.GetType()))
        return (Result.AssignableTypeCast, value);
      
      // Check if the value can be converted to the expected type
      try
      {
        var castValue = Convert.ChangeType(value, expectedType);
        return (Result.ConvertableTypeCast, castValue);
      }
      catch (InvalidCastException)
      {
        return (Result.InvalidTypeCast, value);
      }
      catch (OverflowException)
      {
        return (Result.OverflowTypeCast, value);
      }
    }
  }
}