using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyParserGenerator.Grammar
{
  public class LR0ItemSetCollection : HashSet<LR0ItemSet>
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public LR0ItemSetCollection()
    {
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public override bool Equals(object obj)
    {
      return IsEqual(this, obj as LR0ItemSetCollection);
    }

    public override int GetHashCode()
    {
      return this.Aggregate(0, (current, itemSet) => current ^ itemSet.GetHashCode());
    }

    public void Print()
    {
      int index = 0;

      foreach (var itemSet in this)
      {
        Console.WriteLine($"I{index}");
        itemSet.Print();

        index++;
      }
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Operator Overloads
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public static bool operator ==(LR0ItemSetCollection lhs, LR0ItemSetCollection rhs)
    {
      return IsEqual(lhs, rhs);
    }

    public static bool operator !=(LR0ItemSetCollection lhs, LR0ItemSetCollection rhs)
    {
      return !IsEqual(lhs, rhs);
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Static Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    private static bool IsEqual(LR0ItemSetCollection lhs, LR0ItemSetCollection rhs)
    {
      if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
        return true;

      if (Object.ReferenceEquals(lhs, null) || Object.ReferenceEquals(rhs, null))
        return false;

      // Both sets must have the same number of items
      if (lhs.Count != rhs.Count)
        return false;

      // All the items from lhs must be in rhs
      foreach (LR0ItemSet itemSet in lhs)
      {
        if (!rhs.Contains(itemSet))
          return false;
      }

      return true;
    }
  }
}
