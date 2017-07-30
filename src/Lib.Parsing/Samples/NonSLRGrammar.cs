#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PGSG.Grammar;
#endregion

namespace PGSG.Samples
{
  public class NonSLRGrammar : PGSG.Grammar.Grammar
  {
    // Definitions

    private readonly NonTerminal _sprime = null;
    private readonly NonTerminal _s = null;
    private readonly NonTerminal _l = null;
    private readonly NonTerminal _r = null;

    private readonly Terminal _EQUAL = null;
    private readonly Terminal _STAR = null;
    private readonly Terminal _ID = null;

    // Constructors

    public NonSLRGrammar()
    {
      _sprime = new NonTerminal(this, "sprime");
      _s = new NonTerminal(this, "s");
      _l = new NonTerminal(this, "l");
      _r = new NonTerminal(this, "r");
      _EQUAL = new Terminal(this, "EQUAL");
      _STAR = new Terminal(this, "STAR");
      _ID = new Terminal(this, "ID");

      NonTerminals.Add(_s);
      NonTerminals.Add(_l);
      NonTerminals.Add(_r);

      Terminals.Add(EQUAL);
      Terminals.Add(STAR);
      Terminals.Add(ID);

      Production root;
      Productions.Add(root = new Production(this, sprime, s, EndOfInput)); // s' -> s $
      Productions.Add(new Production(this, s, l, EQUAL, r));               // s  -> l = r
      Productions.Add(new Production(this, s, r));                         // s  -> r
      Productions.Add(new Production(this, l, STAR, r));                   // l  -> * r
      Productions.Add(new Production(this, l, ID));                        // l  -> id
      Productions.Add(new Production(this, r, l));                         // r  -> l

      this.AugmentedProduction = root;
    }

    // Properties

    public NonTerminal sprime
    {
      get
      {
        return _sprime;
      }
    }

    public NonTerminal s
    {
      get
      {
        return _s;
      }
    }

    public NonTerminal l
    {
      get
      {
        return _l;
      }
    }

    public NonTerminal r
    {
      get
      {
        return _r;
      }
    }

    public Terminal EQUAL
    {
      get
      {
        return _EQUAL;
      }
    }

    public Terminal STAR
    {
      get
      {
        return _STAR;
      }
    }

    public Terminal ID
    {
      get
      {
        return _ID;
      }
    }
  }
}

