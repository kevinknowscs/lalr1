using System.Collections.Generic;

using ToyParserGenerator.Grammar;

namespace ToyParserGenerator.Parser
{
  public class ParserState
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public ParserState(LR0ItemSet associatedItemSet)
    {
      AssociatedItemSet = associatedItemSet;
      Actions = new Dictionary<Terminal, ParserAction>();
      Gotos = new Dictionary<NonTerminal, ParserState>();
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public LR0ItemSet AssociatedItemSet { get; private set; }

    public Dictionary<Terminal, ParserAction> Actions { get; private set; }

    public Dictionary<NonTerminal, ParserState> Gotos { get; private set; }
  }
}
