using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyParserGenerator.Grammar
{
  public class Production
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public Production(Grammar aGrammar, NonTerminal aLValue, params BnfTerm[] aBnfTerms)
    {
      Grammar = aGrammar;
      LValue = aLValue;
      BnfTerms = new List<BnfTerm>();

      foreach (var bt in aBnfTerms)
        BnfTerms.Add(bt);
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public Grammar Grammar { get; }

    public NonTerminal LValue
    {
      get
      {
        return _lValue;
      }
      set
      {
        _lValue = value;
        _lValue.Productions.Add(this);
      }
    }

    private NonTerminal _lValue = null;

    public List<BnfTerm> BnfTerms
    {
      get;
      set;
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public override bool Equals(object obj)
    {
      return IsEqual(this, obj as Production);
    }

    public override int GetHashCode()
    {
      return LValue.GetHashCode();
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Operator Overloads
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public static bool operator ==(Production lhs, Production rhs)
    {
      return IsEqual(lhs, rhs);
    }

    public static bool operator !=(Production lhs, Production rhs)
    {
      return !IsEqual(lhs, rhs);
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Private Static Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    private static bool IsEqual(Production lhs, Production rhs)
    {
      if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
        return true;

      if (Object.ReferenceEquals(lhs, null) || Object.ReferenceEquals(rhs, null))
        return false;

      if (lhs.LValue != rhs.LValue)
        return false;

      if (lhs.BnfTerms.Count != rhs.BnfTerms.Count)
        return false;

      return !lhs.BnfTerms.Where((t, x) => t != rhs.BnfTerms[x]).Any();
    }
  }
}
