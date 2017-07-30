#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace PGSG.Grammar
{
  public class EndOfInputTerminal : Terminal
  {
    // Constructors

    public EndOfInputTerminal(Grammar grammar, string name) : base(grammar, name)
    {
    }

    // Property

    public override bool IsEndOfInput
    {
      get
      {
        return true;
      }
    }
  }
}
