using System.Collections.Generic;
using System.Linq;

namespace ToyParserGenerator.Grammar
{
  public class Grammar
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Contructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public Grammar()
    {
      Productions = new List<Production>();
      Terminals = new List<Terminal>();
      NonTerminals = new List<NonTerminal>();
      Empty = new EmptyTerminal(this, "empty");
      EndOfInput = new EndOfInputTerminal(this, "end_of_input");
      Terminals.Add(Empty);
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public Production AugmentedProduction { get; set; }

    public List<Production> Productions { get; set; }

    public List<Terminal> Terminals { get; set; }

    public List<NonTerminal> NonTerminals { get; set; }

    public IEnumerable<BnfTerm> AllSymbols
    {
      get
      {
        foreach (var nt in NonTerminals)
          yield return nt;

        foreach (var t in Terminals)
          yield return t;
      }
    }

    public EmptyTerminal Empty { get; }

    public EndOfInputTerminal EndOfInput { get; }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public LR0ItemSetCollection GetCanonicalCollection()
    {
      var result = new LR0ItemSetCollection();

      var rootItem = new LR0Item(this, this.AugmentedProduction, 0);
      var rootItemClosure = rootItem.GetClosure();

      result.Add(rootItemClosure);

      // Keep track of what we need to do, and what productions we've already added
      var toSearch = new Queue<LR0ItemSet>();
      toSearch.Enqueue(rootItemClosure);

      // Add new item sets until there is nothing left to search
      while (toSearch.Count > 0)
      {
        var current = toSearch.Dequeue();

        // Compute the goto set for each grammar symbol
        foreach (var bnfTerm in AllSymbols)
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

    public void BuildFirstAndFollowSets()
    {
      ClearFirstAndFollow();
      BuildFirstSets();
      BuildFollowSets();
    }

    public void BuildFirstSets()
    {
      var searchStack = new HashSet<Production>();
      AddFirstSets(AugmentedProduction.LValue, searchStack);
    }

    public void BuildFollowSets()
    {
      AugmentedProduction.LValue.Follow.Add(EndOfInput);

      var searchStack = new HashSet<Production>();
      AddFollowSets(AugmentedProduction.LValue, searchStack);
    }

    public void ClearFirstAndFollow()
    {
      foreach (var bnfTerm in AllSymbols)
      {
        bnfTerm.First.Clear();
        bnfTerm.Follow.Clear();
      }
    }

    public void AddFirstSets(NonTerminal nonTerminal, HashSet<Production> searchStack)
    {
      foreach (var prod in Productions)
      {
        if (prod.LValue != nonTerminal)
          continue; // Not the one we're looking for

        if (searchStack.Contains(prod))
          continue; // Skip this one to avoid infinite loops

        foreach (var bnfTerm in prod.BnfTerms)
        {
          var terminal = bnfTerm as Terminal;
          var searchNonTerm = bnfTerm as NonTerminal;

          if (terminal != null)
          {
            nonTerminal.First.Add(terminal);
            break;
          }
          else if (searchNonTerm != null)
          {
            searchStack.Add(prod);
            AddFirstSets(searchNonTerm, searchStack);
            searchStack.Remove(prod);

            // Add all of those first items to the current set of first items
            nonTerminal.First.UnionWith(searchNonTerm.First);

            // If it doesn't contain the empty terminal, then stop
            if (!searchNonTerm.First.Contains(Empty))
              break;
          }
        }
      }
    }

    public void AddFollowSets(NonTerminal nonTerminal, HashSet<Production> searchStack)
    {
      foreach (var prod in Productions)
      {
        if (prod.LValue != nonTerminal)
          continue; // Not the one we're looking for

        if (searchStack.Contains(prod))
          continue; // Skip this one to avoid infinite loops

        var accumulatedFirstSet = new HashSet<Terminal>();

        // Iterate backwards through the BNF terms collection
        for (var x = prod.BnfTerms.Count - 1; x >= 0; x--)
        {
          var bnfTerm = prod.BnfTerms[x];

          // All of the non-empty items of the accumulated first
          // set get added to the follow set of the current symbol
          bnfTerm.Follow.UnionWith(accumulatedFirstSet.Where(t => !t.IsEmpty));

          var terminal = bnfTerm as Terminal;
          var searchNonTerm = bnfTerm as NonTerminal;

          if (terminal != null)
          {
            accumulatedFirstSet.Clear();
            accumulatedFirstSet.Add(terminal);
          }
          else
          {
            searchStack.Add(prod);
            AddFollowSets(searchNonTerm, searchStack);
            searchStack.Remove(prod);

            if (searchNonTerm != null && searchNonTerm.First.Contains(Empty))
              accumulatedFirstSet.UnionWith(searchNonTerm.First);
            else
              accumulatedFirstSet.Clear();
          }
        }

        // Iterate backwards through the BNF terms collection (again)
        for (var x = prod.BnfTerms.Count - 1; x >= 0; x--)
        {
          var bnfTerm = prod.BnfTerms[x];

          var terminal = bnfTerm as Terminal;
          var searchNonTerm = bnfTerm as NonTerminal;

          if (terminal != null)
            break;

          if (searchNonTerm == null)
            continue;

          searchNonTerm.Follow.UnionWith(nonTerminal.Follow);

          if (!searchNonTerm.First.Contains(Empty))
            break;
        }
      }
    }
  }
}
