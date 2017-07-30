#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace PGSG.Grammar
{
  public abstract class BnfTerm
  {
    // Constructors

    public BnfTerm(Grammar grammar, string name)
    {
      Name = name;
      Guid = Guid.NewGuid();
      Grammar = grammar;

      First = new HashSet<Terminal>();
      Follow = new HashSet<Terminal>();
    }

    // Properties

    public string Name
    {
      get;
      set;
    }

    public Guid Guid
    {
      get;
      set;
    }

    public Grammar Grammar
    {
      get;
      set;
    }

    public virtual bool IsEmpty
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsEndOfInput
    {
      get
      {
        return false;
      }
    }

    public HashSet<Terminal> First
    {
      get;
      private set;
    }

    public HashSet<Terminal> Follow
    {
      get;
      private set;
    }
  }
}
