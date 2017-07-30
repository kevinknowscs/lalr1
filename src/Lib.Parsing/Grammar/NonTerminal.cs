#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace PGSG.Grammar
{
  public class NonTerminal : BnfTerm
  {
    // Constructors

    public NonTerminal(Grammar grammar, string name) : base(grammar, name)
    {
      Productions = new List<Production>();
    }

    // Properties

    public List<Production> Productions
    {
      get;
      set;
    }
  }
}
