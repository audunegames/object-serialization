using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines extension methods for objects to cast them to a different type.
  /// </summary>
  internal static class Caster
  {
    /// <summary>
    /// Enum that defines the result of a type cast.
    /// </summary>
    public enum Result
    {
      /// <summary>
      /// The value is null.
      /// </summary>
      Null,
      
      /// <summary>
      /// The value is of the same type as the expected type.
      /// </summary>
      SameTypeCast,
      
      /// <summary>
      /// The type of the value is assignable from the expected type.
      /// </summary>
      AssignableTypeCast,
      
      /// <summary>
      /// The value is convertable from the expected type.
      /// </summary>
      ConvertableTypeCast,
      
      /// <summary>
      /// The type cast is invalid.
      /// </summary>
      InvalidTypeCast,
      
      /// <summary>
      /// The type cast caused an overflow.
      /// </summary>
      OverflowTypeCast,
    }


    /// <summary>
    /// Return in what way the specified value can be cast to the expected type
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="expectedType">The expected type of the type cast.</param>
    /// <returns>The result of the type cast.</returns>
    public static Result TryCast(this object value, Type expectedType)
    {
      return TryCastInternal(value, expectedType).result;
    }

    /// <summary>
    /// Return in what way the specified value can be cast to the expected type and store the cast value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="castValue">The cast value.</param>
    /// <typeparam name="TObject">The expected type of the type cast.</typeparam>
    /// <returns>The result of the type cast.</returns>
    public static Result TryCast<TObject>(this object value, out TObject castValue)
    {
      var cast = TryCastInternal(value, typeof(TObject));
      castValue = cast.result.IsSuccessful() ? (TObject)cast.castValue : default;
      return cast.result;
    }

    /// <summary>
    /// Return if the result of a type cast was successful.
    /// </summary>
    /// <param name="result">The result of a type case.</param>
    /// <returns>If the result of the type cast was successful.</returns>
    public static bool IsSuccessful(this Result result)
    {
      return result == Result.Null || result == Result.SameTypeCast || result == Result.AssignableTypeCast || result == Result.ConvertableTypeCast;
    }
    

    /// <summary>
    /// Return in what way the specified value can be cast to the expected type and return the cast value.
    /// </summary>
    /// <param name="value">The value to cast.</param>
    /// <param name="expectedType">The expected type of the type cast.</param>
    /// <returns>A tuple containing the result of the type cast and the cast value.</returns>
    private static (Result result, object castValue) TryCastInternal(object value, Type expectedType)
    {
      // Check if the value is null
      if (value == null && expectedType.IsClass)
        return (Result.Null, null);

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