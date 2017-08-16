// ReSharper disable InconsistentNaming

using ToyParserGenerator.Grammar;

namespace ToyParserGenerator.Samples
{
  public class SimpleExprGrammar : Grammar.Grammar
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Definitions
    // ////////////////////////////////////////////////////////////////////////////////////////////

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public SimpleExprGrammar()
    {
      eprime = new NonTerminal(this, "eprime");
      e = new NonTerminal(this, "e");
      t = new NonTerminal(this, "t");
      f = new NonTerminal(this, "f");
      PLUS = new Terminal(this, "PLUS");
      STAR = new Terminal(this, "STAR");
      LPAREN = new Terminal(this, "LPAREN");
      RPAREN = new Terminal(this, "RPAREN");
      ID = new Terminal(this, "ID");

      NonTerminals.Add(e);
      NonTerminals.Add(t);
      NonTerminals.Add(f);

      Terminals.Add(PLUS);
      Terminals.Add(STAR);
      Terminals.Add(LPAREN);
      Terminals.Add(RPAREN);
      Terminals.Add(ID);

      Production root;
      Productions.Add(root = new Production(this, eprime, e, EndOfInput)); // e' -> e $
      Productions.Add(new Production(this, e, e, PLUS, t));                // e  -> e + t
      Productions.Add(new Production(this, e, t));                         // e  -> t
      Productions.Add(new Production(this, t, t, STAR, f));                // t  -> t * f
      Productions.Add(new Production(this, t, f));                         // t  -> f
      Productions.Add(new Production(this, f, LPAREN, e, RPAREN));         // f  -> ( e )
      Productions.Add(new Production(this, f, ID));                        // f  -> id

      this.AugmentedProduction = root;
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    // Yes, these are inconsistent naming conventions compared with ordinary C# code. However,
    // since we are writing a grammar description, we favor a naming convention that is more
    // consistent with typical BNF notation and Yacc/Bison conventions, which allows us to more
    // easily distinguish terminals from non-terminals.

    public NonTerminal eprime { get; } = null;

    public NonTerminal e { get; } = null;

    public NonTerminal t { get; } = null;

    public NonTerminal f { get; } = null;

    public Terminal PLUS { get; } = null;

    public Terminal STAR { get; } = null;

    public Terminal LPAREN { get; } = null;

    public Terminal RPAREN { get; } = null;

    public Terminal ID { get; } = null;
  }
}
