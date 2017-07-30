#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PGSG.Grammar;
#endregion

namespace PGSG.Parser
{
  public enum ParserActionTypes
  {
    Shift = 0,
    Reduce = 1,
    Error = 2,
    Accept = 3
  }
}
