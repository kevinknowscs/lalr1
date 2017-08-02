using System.Collections.Generic;
using System.Linq;

using ToyParserGenerator.Grammar;

namespace ToyParserGenerator.Parser
{
  public class ParserData
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public ParserData(Grammar.Grammar grammar)
    {
      Grammar = grammar;
      StateTable = new HashSet<ParserState>();
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public Grammar.Grammar Grammar { get; }

    public HashSet<ParserState> StateTable { get; }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public void Build()
    {
      var states = new Dictionary<LR0ItemSet, ParserState>();

      var cc = Grammar.GetCanonicalCollection();
      Grammar.BuildFirstAndFollowSets();

      foreach (var itemSet in cc)
      {
        var state = new ParserState(itemSet);
        states.Add(itemSet, state);
        StateTable.Add(state);
      }

      foreach (var itemSet in cc)
      {
        var targetState = states[itemSet];

        foreach (var t in Grammar.Terminals)
        {
          foreach (var item in itemSet)
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

        foreach (var nt in Grammar.NonTerminals)
        {
          var gotoSet = itemSet.GetGotoSet(nt);
          if (gotoSet == null || gotoSet.Count == 0)
            continue;

          if (!cc.Contains(gotoSet))
            continue; // Shouldn't happen

          targetState.Gotos[nt] = states[gotoSet];
        }

        foreach (var item in itemSet)
        {
          if (item.NextTerm != null)
            continue;

          foreach (var followTerm in item.Production.LValue.Follow)
          {
            targetState.Actions[followTerm] = ParserAction.CreateReduce(item.Production);
          }
        }
      }
    }

    public ParserState FindState(LR0ItemSet associatedItemSet)
    {
      return StateTable.FirstOrDefault(ps => ps.AssociatedItemSet == associatedItemSet);
    }
  }
}
