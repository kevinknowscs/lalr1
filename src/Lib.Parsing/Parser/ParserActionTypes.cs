#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ToyParserGenerator.Grammar;
#endregion

namespace ToyParserGenerator.Parser
{
  public enum ParserActionTypes
  {
    Shift = 0,
    Reduce = 1,
    Error = 2,
    Accept = 3
  }
}
