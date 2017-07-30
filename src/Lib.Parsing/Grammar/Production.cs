﻿#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace PGSG.Grammar
{
  public class Production
  {
    // Constructors

    public Production(Grammar aGrammar, NonTerminal aLValue, params BnfTerm[] aBnfTerms)
    {
      Grammar = aGrammar;
      LValue = aLValue;
      BnfTerms = new List<BnfTerm>();

      foreach (BnfTerm bt in aBnfTerms)
        BnfTerms.Add(bt);
    }

    // Properties

    public Grammar Grammar
    {
      get;
      set;
    }

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

    // Methods

    public override int GetHashCode()
    {
      return Grammar.GetHashCode() + LValue.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      return IsEqual(this, obj as Production);
    }

    // Operator Overloads

    public static bool operator ==(Production lhs, Production rhs)
    {
      return IsEqual(lhs, rhs);
    }

    public static bool operator !=(Production lhs, Production rhs)
    {
      return !IsEqual(lhs, rhs);
    }

    // Static Methods

    private static bool IsEqual(Production lhs, Production rhs)
    {
      if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
        return true;

      if (Object.ReferenceEquals(lhs, null) || Object.ReferenceEquals(rhs, null))
        return false;

      if (lhs.Grammar != rhs.Grammar)
        return false;

      if (lhs.LValue != rhs.LValue)
        return false;

      if (lhs.BnfTerms.Count != rhs.BnfTerms.Count)
        return false;

      for (int x = 0; x < lhs.BnfTerms.Count; x++)
      {
        if (lhs.BnfTerms[x] != rhs.BnfTerms[x])
          return false;
      }

      return true;
    }
  }
}