using System;
using ToyParserGenerator.Samples;

namespace App.Test
{
  public class Program
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Application Entry Point
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public static void Main(string[] args)
    {
      Console.WriteLine("Creating grammar ...");

      // Pick which grammar to use for testing
      // var grammar = new SimpleExprGrammar();
      var grammar = new NonSLRGrammar();

      Console.WriteLine("Grammar created.");

      var cc = grammar.GetCanonicalCollection();

      Console.WriteLine($"Canonical LR0ItemSet count = {cc.Count}");
      Console.WriteLine();

      cc.Print();
    }
  }
}
