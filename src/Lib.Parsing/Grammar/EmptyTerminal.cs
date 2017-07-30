#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace PGSG.Grammar
{
  public class EmptyTerminal : Terminal
  {
    // Constructors

    public EmptyTerminal(Grammar grammar, string name) : base(grammar, name)
    {
    }

    // Property

    public override bool IsEmpty
    {
      get
      {
        return true;
      }
    }
  }
}
