using System.Collections.Generic;

namespace ToyParserGenerator.Grammar
{
  public class NonTerminal : BnfTerm
  {
    private List<Production> _productions = new List<Production>();

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public NonTerminal(Grammar grammar, string name) : base(grammar, name)
    {
      // Productions = new List<Production>();
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public IEnumerable<Production> Productions
    {
      get
      {
        foreach (var production in _productions)
          yield return production;
      }
    }

    public void AddProduction(Production production)
    {
      _productions.Add(production);
    }
  }
}
