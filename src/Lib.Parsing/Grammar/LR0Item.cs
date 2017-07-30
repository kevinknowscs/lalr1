using System;

namespace ToyParserGenerator.Grammar
{
  public class LR0Item
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public LR0Item(Grammar grammar, Production aProduction, int aDotIndex)
    {
      Grammar = grammar;
      Production = aProduction;
      DotIndex = aDotIndex;
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public Production Production { get; }

    public Grammar Grammar { get; }

    public int DotIndex { get; }

    public BnfTerm PrevTerm => (DotIndex == 0) ? null : Production.BnfTerms[DotIndex - 1];

    public BnfTerm NextTerm => (DotIndex > Production.BnfTerms.Count - 1) ? null : Production.BnfTerms[DotIndex];

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public LR0Item GetNextLR0Item()
    {
      return new LR0Item(Grammar, Production, DotIndex + 1);
    }

    public LR0ItemSet GetClosure()
    {
      // To get the closure of a single LR0Item, add the
      // item to an empty LR0ItemSet and return the closure
      // of the set

      return new LR0ItemSet(Grammar, this).GetClosure();
    }

    public override int GetHashCode()
    {
      return Production.GetHashCode() << 8 + DotIndex;
    }

    public override bool Equals(object obj)
    {
      return IsEqual(this, obj as LR0Item);
    }

    public void Print()
    {
      Console.Write($"{Production.LValue.Name} -> ");

      int index = 0;

      foreach (var term in Production.BnfTerms)
      {
        if (index == DotIndex)
          Console.Write(". ");

        Console.Write(term.Name + " ");
        index++;
      }

      Console.WriteLine();
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Operator Overloads
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public static bool operator ==(LR0Item lhs, LR0Item rhs)
    {
      return IsEqual(lhs, rhs);
    }

    public static bool operator !=(LR0Item lhs, LR0Item rhs)
    {
      return !IsEqual(lhs, rhs);
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Private Static Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    private static bool IsEqual(LR0Item lhs, LR0Item rhs)
    {
      if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
        return true;

      if (Object.ReferenceEquals(lhs, null) || Object.ReferenceEquals(rhs, null))
        return false;

      return lhs.Production == rhs.Production && lhs.DotIndex == rhs.DotIndex;
    }
  }
}
