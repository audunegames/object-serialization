using System.Collections.Generic;

namespace Audune.Pickle
{
  // Interface that defines a list state
  public interface IListState : IEnumerable<State>
  {
    // Return the item count of the list state
    public int count { get; }


    // Get and set the item with the specified index
    public State this[int index] { get => Get(index); set => Set(index, value); }


    // Get an item with the specified index
    public State Get(int index, State defaultValue = null);

    // Return if an item with the specified index exists and store the item
    public bool TryGet(int index, out State state);

    // Add an item
    public void Add(State state);

    // Set an item with the specified index
    public void Set(int index, State value);

    // Remove the item with the specified index
    public void Remove(int index);

    // Return if the specified item exists
    public bool Contains(State value);
  }
}