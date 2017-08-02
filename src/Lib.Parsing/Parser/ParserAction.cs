using ToyParserGenerator.Grammar;

namespace ToyParserGenerator.Parser
{
  public class ParserAction
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Private Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    private ParserAction(ParserActionTypes actionType)
    {
      ActionType = actionType;
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public ParserActionTypes ActionType { get; set; }

    public ParserState NextState { get; set; }

    public Production ReduceProduction { get; set; }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Public Static Methods
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public static ParserAction CreateShift(ParserState nextState)
    {
      return new ParserAction(ParserActionTypes.Shift) { NextState = nextState };
    }

    public static ParserAction CreateReduce(Production reduceProduction)
    {
      return new ParserAction(ParserActionTypes.Reduce) { ReduceProduction = reduceProduction };
    }

    public static ParserAction CreateAccept()
    {
      return null;
    }
  }
}
