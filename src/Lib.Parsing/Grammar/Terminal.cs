#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace PGSG.Grammar
{
  public class Terminal : BnfTerm
  {
    // Constructors

    public Terminal(Grammar grammar, string name) : base(grammar, name)
    {
    }
  }
}
