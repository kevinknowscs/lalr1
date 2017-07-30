using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ToyParserGenerator.Grammar;
using ToyParserGenerator.Samples;


namespace App.Test
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Creating grammar ...");
      SimpleExprGrammar grammar = new SimpleExprGrammar();
      Console.WriteLine("Grammar created.");

      var cc = grammar.GetCanonicalCollection();

      Console.WriteLine("Canonical LR0ItemSet count = {0}", cc.Count);
      Console.WriteLine();

      cc.Print();
    }
  }
}
