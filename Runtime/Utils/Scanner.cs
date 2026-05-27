using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a scanner that iterates over a string.
  /// </summary>
  internal sealed class Scanner
  {
    /// <summary>
    /// The input of the scanner.
    /// </summary>
    private readonly string _input;

    /// <summary>
    /// The index of the character that is currently being scanned.
    /// </summary>
    private int _index = 0;


    /// <summary>
    /// Return the index of the scanner.
    /// </summary>
    public int index => _index;

    /// <summary>
    /// Return if the scanner reached the end of the input string.
    /// </summary>
    public bool atEnd => _index >= _input.Length;

    /// <summary>
    /// Return the character that was just scanned.
    /// </summary>
    public char current => _index > 0 ? _input[_index - 1] : '\0';

    /// <summary>
    /// Return the character that is about to be scanned.
    /// </summary>
    public char next => !atEnd ? _input[_index] : '\0';


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="input">The input of the scanner.</param>
    /// <param name="index">The index where to start scanning.</param>
    public Scanner(string input, int index = 0)
    {
      _input = input ?? string.Empty;
      _index = index;
    }


    #region Scanner functions
    /// <summary>
    /// Advance to the next character and return the just scanned character.
    /// </summary>
    /// <param name="amount">The amount of places to advance.</param>
    /// <returns>The just scanned character.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If amount is not at least 1.</exception>
    public char Advance(int amount = 1)
    {
      if (amount < 1)
        throw new ArgumentOutOfRangeException(nameof(amount), "amount must be at least 1");
      
      if (!atEnd)
        _index += amount;
      return current;
    }

    /// <summary>
    /// Return if the next character matches the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match against.</param>
    /// <returns>If the next character matches the predicate.</returns>
    /// <exception cref="ArgumentNullException">If predicate is null.</exception>
    public bool Check(Predicate<char> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException(nameof(predicate));
      
      return !atEnd && predicate(next);
    }

    /// <summary>
    /// Return if the next character is one of the specified characters.
    /// </summary>
    /// <param name="chars">The characters to match against.</param>
    /// <returns>If the next character is one of the specified characters.</returns>
    public bool Check(params char[] chars)
    {
      return Check(chars.Contains);
    }

    /// <summary>
    /// Return if the next character sequence matches the specified string.
    /// </summary>
    /// <param name="str">The string to match against.</param>
    /// <returns>If the next character sequence matches the specified string.</returns>
    /// <exception cref="ArgumentNullException">If str is null.</exception>
    public bool Check(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof(str));
      
      return !atEnd && _index + str.Length < _input.Length && str == _input[_index..(_index + str.Length)];
    }

    /// <summary>
    /// Return if the next character matches the predicate and advance if so.
    /// </summary>
    /// <param name="predicate">The predicate to match against.</param>
    /// <returns>If the next character matches the predicate.</returns>
    /// <exception cref="ArgumentNullException">If predicate is null.</exception>
    public bool Match(Predicate<char> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException(nameof(predicate));
      
      if (Check(predicate))
      {
        Advance();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Return if the next character is one of the specified characters and advance if so.
    /// </summary>
    /// <param name="chars">The characters to match against.</param>
    /// <returns>If the next character is one of the specified characters.</returns>
    public bool Match(params char[] chars)
    {
      return Match(chars.Contains);
    }

    /// <summary>
    /// Return if the next character sequence matches the specified string and advance if so.
    /// </summary>
    /// <param name="str">The string to match against.</param>
    /// <returns>If the next character sequence matches the specified string.</returns>
    /// <exception cref="ArgumentNullException">If str is null.</exception>
    public bool Match(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof(str));
      
      if (Check(str))
      {
        Advance(str.Length);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Consume the next character if it matches the predicate or throw an error if the consuming was unsuccessful.
    /// </summary>
    /// <param name="predicate">The predicate to match against.</param>
    /// <param name="expected">A string describing the expected format.</param>
    /// <returns>If the next character matches the predicate.</returns>
    /// <exception cref="ArgumentNullException">If predicate is null.</exception>
    /// <exception cref="FormatException">If the consuming was unsuccessful.</exception>
    public char Consume(Predicate<char> predicate, string expected)
    {
      if (predicate == null)
        throw new ArgumentNullException(nameof(predicate));
      
      if (Match(predicate))
        return current;
      else
        throw new FormatException($"Expected {expected}, but found {(!atEnd ? $"'{_input[_index]}'" : "end of string")} at index {_index}");
    }

    /// <summary>
    /// Consume the next character if it is one of the specified characters or throw an error if the consuming was unsuccessful.
    /// </summary>
    /// <param name="chars">The characters to match against.</param>
    /// <returns>If the next character is one of the specified characters.</returns>
    /// <exception cref="FormatException">If the consuming was unsuccessful.</exception>
    public char Consume(params char[] chars)
    {
      return Consume(chars.Contains, chars.Length > 1 ? $"one of {string.Join(", ", chars.Select(c => $"'{c}'"))}" : $"'{chars[0]}'");
    }

    /// <summary>
    /// Consume the next character sequence if it matches the specified string or throw an error if the consuming was unsuccessful.
    /// </summary>
    /// <param name="str">The string to match against.</param>
    /// <returns>If the next character matches the specified string.</returns>
    /// <exception cref="ArgumentNullException">If str is null.</exception>
    /// <exception cref="FormatException">If the consuming was unsuccessful.</exception>
    public string Consume(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof(str));
      
      var start = _index;
      if (Match(str))
        return _input[start..(_index - 1)];
      else
        throw new FormatException($"Expected \"{str}\"{(!atEnd ? ", but found end of string" : "")} at index {_index}");
    }

    /// <summary>
    /// Consume the next character while it matches the predicate and return the consumed characters.
    /// </summary>
    /// <param name="predicate">The predicate to match against.</param>
    /// <returns>A string containing the consumed characters.</returns>
    /// <exception cref="ArgumentNullException">If predicate is null.</exception>
    public string ReadWhile(Predicate<char> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException(nameof(predicate));
      
      var builder = new StringBuilder();
      while (Match(predicate))
        builder.Append(current);
      return builder.ToString();
    }

    /// <summary>
    /// Consume the next character while it is one of the specified characters and return the consumed characters.
    /// </summary>
    /// <param name="chars">The characters to match against.</param>
    /// <returns>A string containing the consumed characters.</returns>
    public string ReadWhile(params char[] chars)
    {
      return ReadWhile(chars.Contains);
    }

    /// <summary>
    /// Consume the next character while it matches the predicate with a special case for the first character and return the consumed characters.
    /// </summary>
    /// <param name="firstPredicate">The predicate to match the first character against.</param>
    /// <param name="firstExpected">A string describing the expected format of the first character.</param>
    /// <param name="restPredicate">The predicate to match the rest of the characters against.</param>
    /// <returns>A string containing the consumed characters.</returns>
    /// <exception cref="ArgumentNullException">If firstPredicate, firstExpected, or restPredicate are null.</exception>
    public string ReadWhile(Predicate<char> firstPredicate, string firstExpected, Predicate<char> restPredicate)
    {
      if (firstPredicate == null)
        throw new ArgumentNullException(nameof(firstPredicate));
      if (firstExpected == null)
        throw new ArgumentNullException(nameof(firstExpected));
      if (restPredicate == null)
        throw new ArgumentNullException(nameof(restPredicate));
      
      var str = new string(Consume(firstPredicate, firstExpected), 1);
      str += ReadWhile(restPredicate);
      return str;
    }

    /// <summary>
    /// Consume the next character while it is one of the specified characters with a special case for the first character and return the consumed characters.
    /// </summary>
    /// <param name="firstChars">The characters to match the first character against.</param>
    /// <param name="restChars">The characters to match the rest of the characters against.</param>
    /// <returns>A string containing the consumed characters.</returns>
    public string ReadWhile(char[] firstChars, char[] restChars)
    {
      Consume(firstChars);
      return ReadWhile(restChars);
    }

    /// <summary>
    /// Consume the next character while it matches the predicate without saving the consumed characters.
    /// </summary>
    /// <param name="predicate">The predicate to match against.</param>
    /// <exception cref="ArgumentNullException">If predicate is null.</exception>
    public void SkipWhile(Predicate<char> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException(nameof(predicate));
      
      while (true)
      {
        if (!Match(predicate))
          break;
      }
    }

    /// <summary>
    /// Consume the next character while it is one of the specified characters without saving the consumed characters.
    /// </summary>
    /// <param name="chars">The characters to match against.</param>
    public void SkipWhile(params char[] chars)
    {
      SkipWhile(chars.Contains);
    }

    /// <summary>
    /// Assert that the scanner has reached the end of the input string.
    /// </summary>
    /// <exception cref="FormatException">If the scanner has not reached the end of the input string.</exception>
    public void AssertAtEnd()
    {
      if (!atEnd)
        throw new FormatException($"Expected end of string, but found '{next}' at index {_index}");
    }
    #endregion

    #region Scanner predicates
    /// <summary>
    /// Return if a character is whitespace.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If the character is whitespace.</returns>
    public static bool IsWhitespace(char c) => c == ' ';

    /// <summary>
    /// Return if a character is a lowercase letter.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If thr character is a lowercase letter.</returns>
    public static bool IsLowercaseLetter(char c) => c >= 'a' && c <= 'z';

    /// <summary>
    /// Return if a character is an uppercase letter.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If the character is an uppercase letter.</returns>
    public static bool IsUppercaseLetter(char c) => c >= 'A' && c <= 'Z';

    /// <summary>
    /// Return if a character is a letter.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If the character is a letter.</returns>
    public static bool IsLetter(char c) => IsLowercaseLetter(c) || IsUppercaseLetter(c);

    /// <summary>
    /// Return if a character is a non-zero digit.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If the character is a non-zero digit.</returns>
    public static bool IsNonZeroDigit(char c) => c >= '1' && c <= '9';

    /// <summary>
    /// Return if a character is a digit.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If the character is a digit.</returns>
    public static bool IsDigit(char c) => IsNonZeroDigit(c) || c == '0';

    /// <summary>
    /// Return if a character is a letter or digit.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If the character is a letter or digit.</returns>
    public static bool IsLetterOrDigit(char c) => IsLetter(c) || IsDigit(c);

    /// <summary>
    /// Return if a character is a letter or an underscore.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If the character is a letter or an underscore.</returns>
    public static bool IsLetterOrUnderscore(char c) => IsLetter(c) || c == '_';

    /// <summary>
    /// Return if a character is a letter, a digit, or an underscore.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>If the character is a letter, a digit, or an underscore.</returns>
    public static bool IsLetterOrDigitOrUnderscore(char c) => IsLetter(c) || IsDigit(c) || c == '_';
    #endregion

    #region Higher-order scanner functions
    /// <summary>
    /// Read an identifier string.
    /// </summary>
    /// <returns>The read identifier string.</returns>
    public string ReadIdentifier()
    {
      return ReadWhile(IsLetterOrUnderscore, "letter or underscore", IsLetterOrDigitOrUnderscore);
    }

    /// <summary>
    /// Read an integer.
    /// </summary>
    /// <returns>The read integer.</returns>
    /// <exception cref="FormatException">If the integer could not be parsed.</exception>
    public int ReadInteger()
    {
      var builder = new StringBuilder();

      if (Match('0'))
        return 0;
      else
        builder.Append(ReadWhile(IsNonZeroDigit, "non-zero digit", IsDigit));

      var value = builder.ToString();
      if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intValue))
        throw new FormatException($"Number \"{value}\" has an invalid integer format");

      return intValue;
    }
    #endregion
  }
}