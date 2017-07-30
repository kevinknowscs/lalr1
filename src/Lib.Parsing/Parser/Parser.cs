#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PGSG.Grammar;
#endregion

namespace PGSG.Parser
{
  public class Parser
  {
    // Constructors

    public Parser(ParserData parserData)
    {
      ParserData = parserData;
    }

    // Properties

    public ParserData ParserData
    {
      get;
      private set;
    }

    // Methods

    public void Parse()
    {
    }
  }
}
