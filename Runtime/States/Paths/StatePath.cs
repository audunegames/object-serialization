using System;

namespace Audune.Serialization
{
  /// <summary>
  /// Class that defines a path in a state.
  /// </summary>
  public abstract class StatePath
  {
    /// <summary>
    /// Evaluate the path as a getter on the specified state.
    /// </summary>
    /// <param name="root">The root state to evaluate the path on.</param>
    /// <returns>The retrieved state at the path.</returns>
    internal abstract State EvaluateGetter(State root);

    /// <summary>
    /// Evaluate the path as a setter on the specified state.
    /// </summary>
    /// <param name="root">The root state to evaluate the path on.</param>
    /// <param name="value">The state to set as the path.</param>
    internal abstract void EvaluateSetter(State root, State value);


    /// <summary>
    /// Parse a path.
    /// </summary>
    /// <param name="input">The input to parse.</param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static StatePath Parse(string input)
    {
      var path = default(StatePath);

      var scanner = new Scanner(input);
      while (!scanner.atEnd)
      {
        if (scanner.index > 0 && scanner.Match('['))
        {
          var index = scanner.ReadInteger();
          path = new Indexer(path, index);
          scanner.Consume(']');
        }
        else if (scanner.index == 0 || scanner.Match('.'))
        {
          var name = scanner.ReadIdentifier();
          path = new Accessor(path, name);
        }
        else
        {
          throw new FormatException($"Found invalid character '{scanner.next}' at position {scanner.index}");
        }
      }

      return path;
    }

    /// <summary>
    /// Convert a <see cref="String"/> value to a <see cref="StatePath"/>.
    /// </summary>
    /// <param name="input">The value to convert.</param>
    /// <returns>The converted path.</returns>
    public static implicit operator StatePath(string input) 
    { 
      return Parse(input);
    }


    /// <summary>
    /// Class that defines an indexer path.
    /// </summary>
    internal sealed class Indexer : StatePath
    {
      /// <summary>
      /// The parent of the path.
      /// </summary>
      private readonly StatePath _parent;

      /// <summary>
      /// The index of the path.
      /// </summary>
      private readonly int _index;


      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="parent">The parent of the path.</param>
      /// <param name="index">The index of the path.</param>
      internal Indexer(StatePath parent, int index)
      {
        _parent = parent;
        _index = index;
      }


      /// <inheritdoc/>
      internal override State EvaluateGetter(State root)
      {
        var parentState = _parent?.EvaluateGetter(root) ?? root;

        if (parentState is not IListState listState)
          throw new StatePathException($"Expected state of type {typeof(IListState)} but found {root.GetType()}");
        else if (!listState.TryGet(_index, out var itemState))
          throw new StatePathException($"Undefined item with index {_index}");
        else
          return itemState;
      }

      /// <inheritdoc/>
      internal override void EvaluateSetter(State root, State value)
      {
        var parentState = _parent?.EvaluateGetter(root) ?? root;

        if (parentState is not IListState listState)
          throw new StatePathException($"Expected state of type {typeof(IListState)} but found {root.GetType()}");
        else
          listState.Set(_index, value);
      }
    }


    /// <summary>
    /// Class that defines an accessor path.
    /// </summary>
    internal sealed class Accessor : StatePath
    {
      /// <summary>
      /// The parent of the path.
      /// </summary>
      private readonly StatePath _parent;

      /// <summary>
      /// The name of the path.
      /// </summary>
      private readonly string _name;


      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="parent">The parent of the path.</param>
      /// <param name="name">The name of the path.</param>
      internal Accessor(StatePath parent, string name)
      {
        _parent = parent;
        _name = name;
      }


      /// <inheritdoc/>
      internal override State EvaluateGetter(State root)
      {
        var parentState = _parent?.EvaluateGetter(root) ?? root;

        if (parentState is not IObjectState objectState)
          throw new StatePathException($"Expected state of type {typeof(IObjectState)} but found {root.GetType()}");
        else if (!objectState.TryGet(_name, out var fieldState))
          throw new StatePathException($"Undefined field with name {_name}");
        else 
          return fieldState;
      }

      /// <inheritdoc/>
      internal override void EvaluateSetter(State root, State value)
      {
        var parentState = _parent?.EvaluateGetter(root) ?? root;

        if (parentState is not IObjectState objectState)
          throw new StatePathException($"Expected state of type {typeof(IObjectState)} but found {root.GetType()}");
        else
          objectState.Set(_name, value);
      }
    }
  }
}