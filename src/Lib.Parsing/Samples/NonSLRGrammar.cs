﻿// ReSharper disable InconsistentNaming

using ToyParserGenerator.Grammar;

namespace ToyParserGenerator.Samples
{
  public class NonSLRGrammar : Grammar.Grammar
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public NonSLRGrammar()
    {
      sprime = new NonTerminal(this, "sprime");
      s = new NonTerminal(this, "s");
      l = new NonTerminal(this, "l");
      r = new NonTerminal(this, "r");

      EQUAL = new Terminal(this, "EQUAL");
      STAR = new Terminal(this, "STAR");
      ID = new Terminal(this, "ID");

      NonTerminals.Add(s);
      NonTerminals.Add(l);
      NonTerminals.Add(r);

      Terminals.Add(EQUAL);
      Terminals.Add(STAR);
      Terminals.Add(ID);

      var root = new Production(this, sprime, s, EndOfInput);

      Productions.Add(root);                                 // s' -> s $
      Productions.Add(new Production(this, s, l, EQUAL, r)); // s  -> l = r
      Productions.Add(new Production(this, s, r));           // s  -> r
      Productions.Add(new Production(this, l, STAR, r));     // l  -> * r
      Productions.Add(new Production(this, l, ID));          // l  -> id
      Productions.Add(new Production(this, r, l));           // r  -> l

      this.AugmentedProduction = root;
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    // Yes, these are inconsistent naming conventions compared with ordinary C# code. However,
    // since we are writing a grammar description, we favor a naming convention that is more
    // consistent with typical BNF notation and Yacc/Bison conventions, which allows us to more
    // easily distinguish terminals from non-terminals.

    public NonTerminal sprime { get; } = null;

    public NonTerminal s { get; } = null;

    public NonTerminal l { get; } = null;

    public NonTerminal r { get; } = null;

    public Terminal EQUAL { get; } = null;

    public Terminal STAR { get; } = null;

    public Terminal ID { get; } = null;
  }
}

