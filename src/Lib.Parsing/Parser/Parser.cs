#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ToyParserGenerator.Grammar;
#endregion

namespace ToyParserGenerator.Parser
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
