#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyParserGenerator.Grammar;

#endregion

namespace ToyParserGenerator.Samples
{
  public class SimpleExprGrammar : Grammar.Grammar
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Definitions
    // ////////////////////////////////////////////////////////////////////////////////////////////

    private readonly NonTerminal _e = null;
    private readonly NonTerminal _t = null;
    private readonly NonTerminal _f = null;

    private readonly Terminal _PLUS = null;
    private readonly Terminal _STAR = null;
    private readonly Terminal _LPAREN = null;
    private readonly Terminal _RPAREN = null;
    private readonly Terminal _ID = null;

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public SimpleExprGrammar()
    {
      eprime = new NonTerminal(this, "eprime");
      _e = new NonTerminal(this, "e");
      _t = new NonTerminal(this, "t");
      _f = new NonTerminal(this, "f");
      _PLUS = new Terminal(this, "PLUS");
      _STAR = new Terminal(this, "STAR");
      _LPAREN = new Terminal(this, "LPAREN");
      _RPAREN = new Terminal(this, "RPAREN");
      _ID = new Terminal(this, "ID");

      NonTerminals.Add(_e);
      NonTerminals.Add(_t);
      NonTerminals.Add(_f);

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

    public NonTerminal eprime { get; } = null;

    public NonTerminal e => _e;

    public NonTerminal t => _t;

    public NonTerminal f => _f;

    public Terminal PLUS => _PLUS;

    public Terminal STAR => _STAR;

    public Terminal LPAREN => _LPAREN;

    public Terminal RPAREN => _RPAREN;

    public Terminal ID => _ID;
  }
}
