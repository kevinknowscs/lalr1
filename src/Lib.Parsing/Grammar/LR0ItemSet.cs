using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyParserGenerator.Grammar
{
  public class LR0ItemSet : HashSet<LR0Item>
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public LR0ItemSet(Grammar grammar, params LR0Item[] items) 
    {
      Grammar = grammar;
      AddRange(items);
    }

    public LR0ItemSet(Grammar grammar, IEnumerable<LR0Item> items)
    {
      Grammar = grammar;
      AddRange(items);
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public Grammar Grammar { get; }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public void AddRange(params LR0Item[] items)
    {
      foreach (var item in items)
        Add(item);
    }

    public void AddRange(IEnumerable<LR0Item> items)
    {
      foreach (var item in items)
        Add(item);
    }

    public bool Contains(Production p, int dotIndex)
    {
      return this.Any(item => item.Production == p && item.DotIndex == dotIndex);
    }

    public LR0ItemSet GetClosure()
    {
      var results = new LR0ItemSet(Grammar);

      // Keep track of what we need to do, and what productions we've already added
      var toSearch = new Queue<LR0Item>();
      var completed = new HashSet<NonTerminal>();

      // Initially, the closure contains all the items of the original set.
      // Seed the search queue with all items from the original set.
      foreach (var item in this)
      {
        results.Add(item);
        toSearch.Enqueue(item);
      }

      // Keep searching for new items to add until there is nothing left to search
      while (toSearch.Count > 0)
      {
        var currentItem = toSearch.Dequeue();

        var nt = currentItem.NextTerm as NonTerminal;
        if (nt == null)
          continue; // For this LR0Item, the dot preceeds a terminal, not a non-terminal, so skip it

        if (completed.Contains(nt))
          continue; // We already did this one

        // Add all the productions to the closure
        foreach (var prod in nt.Productions)
        {
          if (results.Contains(prod, 0))
            continue;

          // Add a new item for this production to the set
          var newItem = new LR0Item(Grammar, prod, 0);
          results.Add(newItem);

          // Now we have to search the new item for yet more productions
          // that might need to be added
          toSearch.Enqueue(newItem);
        }

        // We've now added all the productions for this non-terminal
        completed.Add(nt);
      }

      return results;
    }

    public LR0ItemSet GetGotoSet(BnfTerm bnfTerm)
    {
      var result = new LR0ItemSet(Grammar);

      // Seed the result set
      foreach (var item in this)
      {
        if (item.NextTerm == bnfTerm)
        {
          // The item added to the seed set is essentially the same as the matching
          // LR0Item, except the dot index is moved one space forward
          result.Add(item.GetNextLR0Item());
        }
      }

      return result.GetClosure();
    }

    public LR0ItemSetCollection GetCanonicalCollection()
    {
      var result = new LR0ItemSetCollection();

      var rootItemClosure = this.GetClosure();
      result.Add(rootItemClosure);

      // Keep track of what we need to do, and what productions we've already added
      var toSearch = new Queue<LR0ItemSet>();
      toSearch.Enqueue(rootItemClosure);

      // Add new item sets until there is nothing left to search
      while (toSearch.Count > 0)
      {
        var current = toSearch.Dequeue();

        // Compute the goto set for each grammar symbol
        foreach (var bnfTerm in Grammar.AllSymbols)
        {
          var gotoSet = current.GetGotoSet(bnfTerm);

          if (gotoSet.Count == 0 || result.Contains(gotoSet))
            continue;

          // Add this set to the result collection
          result.Add(gotoSet);

          // Now we have to search this set for more goto sets to add
          toSearch.Enqueue(gotoSet);
        }
      }

      return result;
    }

    public override int GetHashCode()
    {
      return this.Aggregate(0, (current, item) => current ^ item.GetHashCode());
    }

    public override bool Equals(object obj)
    {
      return IsEqual(this, obj as LR0ItemSet);
    }

    public void Print()
    {
      Console.WriteLine($"Set has {Count} item(s).\n");

      foreach (var item in this)
        item.Print();

      Console.WriteLine();
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Operator Overloads
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public static bool operator ==(LR0ItemSet lhs, LR0ItemSet rhs)
    {
      return IsEqual(lhs, rhs);
    }

    public static bool operator !=(LR0ItemSet lhs, LR0ItemSet rhs)
    {
      return !IsEqual(lhs, rhs);
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Static Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public static bool IsEqual(LR0ItemSet lhs, LR0ItemSet rhs)
    {
      if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
        return true;

      if (Object.ReferenceEquals(lhs, null) || Object.ReferenceEquals(rhs, null))
        return false;

      // All the items from lhs must be in rhs and vice versa
      return (lhs.Count == rhs.Count) && lhs.All(rhs.Contains) && rhs.All(lhs.Contains);
    }
  }
}
