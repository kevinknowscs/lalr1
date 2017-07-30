#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PGSG.Grammar;
using PGSG.Parser;
using PGSG.Samples;
#endregion

namespace UnitTests.Parsing
{
  [TestClass]
  public class ParserDataTests
  {
    [TestMethod]
    public void T001_SimpleExprDataTest()
    {
      var grammar = new SimpleExprGrammar();

      var parserData = new ParserData(grammar);
      parserData.Build();

      Assert.IsNotNull(parserData, "T001.01 - Parser data should not be null");
      Assert.IsTrue(parserData.StateTable.Count == 12, "T001.02 - State table should contain 12 states");

      // Productions of the original (non-augmented) grammar

      Production p1 = new Production(grammar, grammar.e, grammar.e, grammar.PLUS, grammar.t);
      Production p2 = new Production(grammar, grammar.e, grammar.t);
      Production p3 = new Production(grammar, grammar.t, grammar.t, grammar.STAR, grammar.f);
      Production p4 = new Production(grammar, grammar.t, grammar.f);
      Production p5 = new Production(grammar, grammar.f, grammar.LPAREN, grammar.e, grammar.RPAREN);
      Production p6 = new Production(grammar, grammar.f, grammar.ID);

      // Augmented production

      Production p0 = new Production(grammar, grammar.eprime, grammar.e, grammar.EndOfInput);

      // Parser State 0

      LR0Item s0_1 = new LR0Item(grammar, p0, 0); // e' -> . e $
      LR0Item s0_2 = new LR0Item(grammar, p1, 0); // e  -> . e + t
      LR0Item s0_3 = new LR0Item(grammar, p2, 0); // e  -> . t
      LR0Item s0_4 = new LR0Item(grammar, p3, 0); // t  -> . t * f
      LR0Item s0_5 = new LR0Item(grammar, p4, 0); // t  -> . f
      LR0Item s0_6 = new LR0Item(grammar, p5, 0); // f  -> . ( e )
      LR0Item s0_7 = new LR0Item(grammar, p6, 0); // f  -> . id

      LR0ItemSet s0_items = new LR0ItemSet(grammar, s0_1, s0_2, s0_3, s0_4, s0_5, s0_6, s0_7);
      ParserState s0 = parserData.FindState(s0_items);

      Assert.IsNotNull(s0);

      // Parser State 1

      LR0Item s1_1 = new LR0Item(grammar, p0, 1); // e' -> e . $
      LR0Item s1_2 = new LR0Item(grammar, p1, 1); // e  -> e . + t

      LR0ItemSet s1_items = new LR0ItemSet(grammar, s1_1, s1_2);
      ParserState s1 = parserData.FindState(s1_items);

      Assert.IsNotNull(s1);

      // Parser State 2

      LR0Item s2_1 = new LR0Item(grammar, p2, 1);
      LR0Item s2_2 = new LR0Item(grammar, p3, 1);

      LR0ItemSet s2_items = new LR0ItemSet(grammar, s2_1, s2_2);
      ParserState s2 = parserData.FindState(s2_items);

      Assert.IsNotNull(s2);

      // Parser State 3

      LR0Item s3_1 = new LR0Item(grammar, p4, 1);

      LR0ItemSet s3_items = new LR0ItemSet(grammar, s3_1);
      ParserState s3 = parserData.FindState(s3_items);

      Assert.IsNotNull(s3);

      // Parser State 4

      LR0Item s4_1 = new LR0Item(grammar, p5, 1);
      LR0Item s4_2 = new LR0Item(grammar, p1, 0);
      LR0Item s4_3 = new LR0Item(grammar, p2, 0);
      LR0Item s4_4 = new LR0Item(grammar, p3, 0);
      LR0Item s4_5 = new LR0Item(grammar, p4, 0);
      LR0Item s4_6 = new LR0Item(grammar, p5, 0);
      LR0Item s4_7 = new LR0Item(grammar, p6, 0);

      LR0ItemSet s4_items = new LR0ItemSet(grammar, s4_1, s4_2, s4_3, s4_4, s4_5, s4_6, s4_7);
      ParserState s4 = parserData.FindState(s4_items);

      Assert.IsNotNull(s4);

      // Parser State 5

      LR0Item s5_1 = new LR0Item(grammar, p6, 1); // f -> id .

      LR0ItemSet s5_items = new LR0ItemSet(grammar, s5_1);
      ParserState s5 = parserData.FindState(s5_items);

      Assert.IsNotNull(s5);

      // Parser State 6

      LR0Item s6_1 = new LR0Item(grammar, p1, 2);
      LR0Item s6_2 = new LR0Item(grammar, p3, 0);
      LR0Item s6_3 = new LR0Item(grammar, p4, 0);
      LR0Item s6_4 = new LR0Item(grammar, p5, 0);
      LR0Item s6_5 = new LR0Item(grammar, p6, 0);

      LR0ItemSet s6_items = new LR0ItemSet(grammar, s6_1, s6_2, s6_3, s6_4, s6_5);
      ParserState s6 = parserData.FindState(s6_items);

      Assert.IsNotNull(s6);

      // Parser State 7

      LR0Item s7_1 = new LR0Item(grammar, p3, 2);
      LR0Item s7_2 = new LR0Item(grammar, p5, 0);
      LR0Item s7_3 = new LR0Item(grammar, p6, 0);

      LR0ItemSet s7_items = new LR0ItemSet(grammar, s7_1, s7_2, s7_3);
      ParserState s7 = parserData.FindState(s7_items);

      // Parser State 8

      LR0Item s8_1 = new LR0Item(grammar, p5, 2);
      LR0Item s8_2 = new LR0Item(grammar, p1, 1);

      LR0ItemSet s8_items = new LR0ItemSet(grammar, s8_1, s8_2);
      ParserState s8 = parserData.FindState(s8_items);

      Assert.IsNotNull(s8);

      // Parser State 9

      LR0Item s9_1 = new LR0Item(grammar, p1, 3);
      LR0Item s9_2 = new LR0Item(grammar, p3, 1);

      LR0ItemSet s9_items = new LR0ItemSet(grammar, s9_1, s9_2);
      ParserState s9 = parserData.FindState(s9_items);

      Assert.IsNotNull(s9);

      // Parser State 10

      LR0Item s10_1 = new LR0Item(grammar, p3, 3);

      LR0ItemSet s10_items = new LR0ItemSet(grammar, s10_1);
      ParserState s10 = parserData.FindState(s10_items);

      Assert.IsNotNull(s10);

      // Parser State 11

      LR0Item s11_1 = new LR0Item(grammar, p5, 3);

      LR0ItemSet s11_items = new LR0ItemSet(grammar, s11_1);
      ParserState s11 = parserData.FindState(s11_items);

      Assert.IsNotNull(s11);

      ParserAction action;

      // State 0 Action Tests

      Assert.IsTrue(s0.Actions.Count == 2);

      action = s0.Actions[grammar.ID];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s5);

      action = s0.Actions[grammar.LPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s4);

      // State 1 Action Tests

      Assert.IsTrue(s1.Actions.Count == 1);

      action = s1.Actions[grammar.PLUS];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s6);

      // State 2 Action Tests

      Assert.IsTrue(s2.Actions.Count == 4);

      action = s2.Actions[grammar.STAR];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s7);

      action = s2.Actions[grammar.PLUS];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p2);

      action = s2.Actions[grammar.RPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p2);

      action = s2.Actions[grammar.EndOfInput];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p2);

      // State 3 Action Tests

      Assert.IsTrue(s3.Actions.Count == 4);

      action = s3.Actions[grammar.STAR];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p4);

      action = s3.Actions[grammar.PLUS];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p4);

      action = s3.Actions[grammar.RPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p4);

      action = s3.Actions[grammar.EndOfInput];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p4);

      // State 4 Action Tests

      Assert.IsTrue(s4.Actions.Count == 2);

      action = s4.Actions[grammar.ID];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s5);

      action = s4.Actions[grammar.LPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s4);

      // State 5 Action Tests

      Assert.IsTrue(s3.Actions.Count == 4);

      action = s5.Actions[grammar.STAR];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p6);

      action = s5.Actions[grammar.PLUS];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p6);

      action = s5.Actions[grammar.RPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p6);

      action = s5.Actions[grammar.EndOfInput];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p6);

      // State 6 Action Tests

      Assert.IsTrue(s6.Actions.Count == 2);

      action = s6.Actions[grammar.ID];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s5);

      action = s6.Actions[grammar.LPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s4);

      // State 7 Action Tests

      Assert.IsTrue(s7.Actions.Count == 2);

      action = s7.Actions[grammar.ID];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s5);

      action = s7.Actions[grammar.LPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s4);

      // State 9 Action Tests

      Assert.IsTrue(s9.Actions.Count == 4);

      action = s9.Actions[grammar.STAR];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Shift);
      Assert.IsTrue(action.NextState == s7);

      action = s9.Actions[grammar.PLUS];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p1);

      action = s9.Actions[grammar.RPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p1);

      action = s9.Actions[grammar.EndOfInput];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p1);

      // State 10 Action Tests

      Assert.IsTrue(s10.Actions.Count == 4);

      action = s10.Actions[grammar.STAR];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p3);

      action = s10.Actions[grammar.PLUS];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p3);

      action = s10.Actions[grammar.RPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p3);

      action = s10.Actions[grammar.EndOfInput];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p3);

      // State 11 Action Tests

      Assert.IsTrue(s11.Actions.Count == 4);

      action = s11.Actions[grammar.STAR];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p5);

      action = s11.Actions[grammar.PLUS];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p5);

      action = s11.Actions[grammar.RPAREN];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p5);

      action = s11.Actions[grammar.EndOfInput];
      Assert.IsNotNull(action);
      Assert.IsTrue(action.ActionType == ParserActionTypes.Reduce);
      Assert.IsTrue(action.ReduceProduction == p5);

      ParserState gotoState;

      // State 0 Goto Tests

      Assert.IsTrue(s0.Gotos.Count == 3);

      gotoState = s0.Gotos[grammar.e];
      Assert.IsTrue(gotoState == s1);

      gotoState = s0.Gotos[grammar.t];
      Assert.IsTrue(gotoState == s2);

      gotoState = s0.Gotos[grammar.f];
      Assert.IsTrue(gotoState == s3);

      // State 1 Goto Tests

      Assert.IsTrue(s1.Gotos.Count == 0);

      // State 2 Goto Tests

      Assert.IsTrue(s2.Gotos.Count == 0);

      // State 3 Goto Tests

      Assert.IsTrue(s3.Gotos.Count == 0);

      // State 4 Goto Tests

      Assert.IsTrue(s4.Gotos.Count == 3);

      gotoState = s4.Gotos[grammar.e];
      Assert.IsTrue(gotoState == s8);

      gotoState = s4.Gotos[grammar.t];
      Assert.IsTrue(gotoState == s2);

      gotoState = s4.Gotos[grammar.f];
      Assert.IsTrue(gotoState == s3);

      // State 5 Goto Tests

      Assert.IsTrue(s5.Gotos.Count == 0);

      // State 6 Goto Tests

      Assert.IsTrue(s6.Gotos.Count == 2);

      gotoState = s6.Gotos[grammar.t];
      Assert.IsTrue(gotoState == s9);

      gotoState = s6.Gotos[grammar.f];
      Assert.IsTrue(gotoState == s3);

      // State 7 Goto Tests

      Assert.IsTrue(s7.Gotos.Count == 1);

      gotoState = s7.Gotos[grammar.f];
      Assert.IsTrue(gotoState == s10);

      // State 8 Goto Tests

      Assert.IsTrue(s8.Gotos.Count == 0);

      // State 9 Goto Tests

      Assert.IsTrue(s9.Gotos.Count == 0);

      // State 10 Goto Tests

      Assert.IsTrue(s10.Gotos.Count == 0);

      // State 11 Goto Tests

      Assert.IsTrue(s11.Gotos.Count == 0);
    }

    [TestMethod]
    public void T002_NonSLRDataTest()
    {
      var grammar = new NonSLRGrammar();

      var parserData = new ParserData(grammar);
      parserData.Build();

      Assert.IsTrue(parserData.StateTable.Count == 10);
    }
  }
}