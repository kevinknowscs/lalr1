#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PGSG.Grammar;
#endregion

namespace PGSG.Parser
{
  public class ParserAction
  {
    // Private Constructors

    private ParserAction(ParserActionTypes actionType)
    {
      ActionType = actionType;
    }

    // Static Methods

    public static ParserAction CreateShift(ParserState nextState)
    {
      return new ParserAction(ParserActionTypes.Shift) { NextState = nextState };
    }

    public static ParserAction CreateReduce(Production reduceProduction)
    {
      return new ParserAction(ParserActionTypes.Reduce) { ReduceProduction = reduceProduction };
    }

    public static ParserAction CreateAccept()
    {
      return null;
    }

    // Public Properties

    public ParserActionTypes ActionType
    {
      get;
      set;
    }

    public ParserState NextState
    {
      get;
      set;
    }

    public Production ReduceProduction
    {
      get;
      set;
    }
  }
}
