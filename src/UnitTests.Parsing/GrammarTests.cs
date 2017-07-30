#region using
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ToyParserGenerator.Grammar;
using ToyParserGenerator.Parser;
using ToyParserGenerator.Samples;
#endregion

namespace UnitTests.Parsing
{
  [TestClass]
  public class GrammarTests
  {
    // Tests

    [TestMethod]
    public void T001_LR0ItemTest()
    {
      Grammar grammar = new Grammar();
      var e = new NonTerminal(grammar, "e");
      var t = new NonTerminal(grammar, "t");
      var ID = new Terminal(grammar, "ID");
      var PLUS = new Terminal(grammar, "PLUS");
      Production p1 = new Production(grammar, e, e, PLUS, t);

      LR0Item item1 = new LR0Item(grammar, p1, 0);
      LR0Item item2 = new LR0Item(grammar, p1, 0);
      LR0Item item3 = new LR0Item(grammar, p1, 1);

      Assert.IsTrue(item1.Equals(item2), "T001.01 - Items should be equal");
      Assert.IsTrue(item1 == item2, "T001.02 - Items should be equal");
      Assert.IsFalse(item1 != item2, "T002.03 - Items should be equal");

      Assert.IsFalse(item2.Equals(item3), "T001.04 - Items should not be equal");
      Assert.IsFalse(item2 == item3, "T001.05 - Items should not be equal");
      Assert.IsTrue(item2 != item3, "T002.06 - Items should not be equal");

      var itemSet = new HashSet<LR0Item>();
      itemSet.Add(item1);

      Assert.IsTrue(itemSet.Contains(item2), "T001.07 - Item 2 should be in the set");
    }

    [TestMethod]
    public void T002_SimpleExprGrammarTest()
    {
      var grammar = new SimpleExprGrammar();

      LR0Item rootItem = new LR0Item(grammar, grammar.AugmentedProduction, 0);
      LR0ItemSet i0 = rootItem.GetClosure();

      Assert.IsTrue(i0.Count == 7);

      LR0ItemSet i1 = i0.GetGotoSet(grammar.e);
      Assert.IsTrue(i1.Count == 2);

      LR0ItemSet i2 = i0.GetGotoSet(grammar.t);
      Assert.IsTrue(i2.Count == 2);
    }

    [TestMethod]
    public void T003_CanonicalCollectionTest()
    {
      var grammar = new SimpleExprGrammar();

      var cc = grammar.GetCanonicalCollection();

      Assert.IsTrue(cc.Count == 12);
    }

    [TestMethod]
    public void T004_BuildFirstAndFollowSetsTest()
    {
      var grammar = new SimpleExprGrammar();

      grammar.BuildFirstAndFollowSets();

      Assert.IsTrue(grammar.eprime.First.Count == 2);
      Assert.IsTrue(grammar.eprime.First.Contains(grammar.LPAREN));
      Assert.IsTrue(grammar.eprime.First.Contains(grammar.ID));

      Assert.IsTrue(grammar.eprime.Follow.Count == 1);
      Assert.IsTrue(grammar.eprime.Follow.Contains(grammar.EndOfInput));

      Assert.IsTrue(grammar.e.First.Count == 2);
      Assert.IsTrue(grammar.e.First.Contains(grammar.LPAREN));
      Assert.IsTrue(grammar.e.First.Contains(grammar.ID));

      Assert.IsTrue(grammar.e.Follow.Count == 3);
      Assert.IsTrue(grammar.e.Follow.Contains(grammar.PLUS));
      Assert.IsTrue(grammar.e.Follow.Contains(grammar.RPAREN));
      Assert.IsTrue(grammar.e.Follow.Contains(grammar.EndOfInput));

      Assert.IsTrue(grammar.t.First.Count == 2);
      Assert.IsTrue(grammar.t.First.Contains(grammar.LPAREN));
      Assert.IsTrue(grammar.t.First.Contains(grammar.ID));

      Assert.IsTrue(grammar.t.Follow.Count == 4);
      Assert.IsTrue(grammar.t.Follow.Contains(grammar.PLUS));
      Assert.IsTrue(grammar.t.Follow.Contains(grammar.STAR));
      Assert.IsTrue(grammar.t.Follow.Contains(grammar.RPAREN));
      Assert.IsTrue(grammar.t.Follow.Contains(grammar.EndOfInput));

      Assert.IsTrue(grammar.f.First.Count == 2);
      Assert.IsTrue(grammar.f.First.Contains(grammar.LPAREN));
      Assert.IsTrue(grammar.f.First.Contains(grammar.ID));

      Assert.IsTrue(grammar.f.Follow.Count == 4);
      Assert.IsTrue(grammar.f.Follow.Contains(grammar.PLUS));
      Assert.IsTrue(grammar.f.Follow.Contains(grammar.STAR));
      Assert.IsTrue(grammar.f.Follow.Contains(grammar.RPAREN));
      Assert.IsTrue(grammar.f.Follow.Contains(grammar.EndOfInput));
    }
  }
}