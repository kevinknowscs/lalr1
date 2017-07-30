using System;
using System.Collections.Generic;

namespace PGSG.Grammar
{
  public abstract class BnfTerm
  {
    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Constructors
    // ////////////////////////////////////////////////////////////////////////////////////////////

    protected BnfTerm(Grammar grammar, string name)
    {
      Name = name;
      Guid = Guid.NewGuid();
      Grammar = grammar;

      First = new HashSet<Terminal>();
      Follow = new HashSet<Terminal>();
    }

    // ////////////////////////////////////////////////////////////////////////////////////////////
    // Properties
    // ////////////////////////////////////////////////////////////////////////////////////////////

    public string Name { get; set; }

    public Guid Guid { get; set; }

    public Grammar Grammar { get; set; }

    public virtual bool IsEmpty => false;

    public virtual bool IsEndOfInput => false;

    public HashSet<Terminal> First { get; private set; }

    public HashSet<Terminal> Follow { get; private set; }
  }
}
