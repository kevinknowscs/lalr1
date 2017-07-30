#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyParserGenerator.Grammar;

#endregion

namespace ToyParserGenerator.Parser
{
  public class ParserData
  {
    // Constructors

    public ParserData(Grammar.Grammar grammar)
    {
      Grammar = grammar;
      StateTable = new HashSet<ParserState>();
    }

    // Properties

    public Grammar.Grammar Grammar
    {
      get;
      private set;
    }

    public HashSet<ParserState> StateTable
    {
      get;
      private set;
    }

    // Methods

    public void Build()
    {
      var states = new Dictionary<LR0ItemSet, ParserState>();

      var cc = Grammar.GetCanonicalCollection();
      Grammar.BuildFirstAndFollowSets();

      foreach (LR0ItemSet itemSet in cc)
      {
        var state = new ParserState(itemSet);
        states.Add(itemSet, state);
        StateTable.Add(state);
      }

      foreach (LR0ItemSet itemSet in cc)
      {
        var targetState = states[itemSet];

        foreach (Terminal t in Grammar.Terminals)
        {
          foreach (LR0Item item in itemSet)
          {
            if (item.NextTerm != t)
              continue;

            var gotoSet = itemSet.GetGotoSet(t);
            if (gotoSet == null || gotoSet.Count == 0)
              continue;

            if (!cc.Contains(gotoSet))
              continue; // Shouldn't happen

            targetState.Actions[t] = ParserAction.CreateShift(states[gotoSet]);
          }
        }

        foreach (NonTerminal nt in Grammar.NonTerminals)
        {
          var gotoSet = itemSet.GetGotoSet(nt);
          if (gotoSet == null || gotoSet.Count == 0)
            continue;

          if (!cc.Contains(gotoSet))
            continue; // Shouldn't happen

          targetState.Gotos[nt] = states[gotoSet];
        }

        foreach (LR0Item item in itemSet)
        {
          if (item.NextTerm != null)
            continue;

          foreach (Terminal followTerm in item.Production.LValue.Follow)
          {
            targetState.Actions[followTerm] = ParserAction.CreateReduce(item.Production);
          }
        }
      }
    }

    public ParserState FindState(LR0ItemSet associatedItemSet)
    {
      foreach (ParserState ps in StateTable)
      {
        if (ps.AssociatedItemSet == associatedItemSet)
          return ps;
      }

      return null;
    }
  }
}
