using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Models.Exceptions
{
  public class FightNullException : Exception
  {
    public FightNullException() : base() { }
    public FightNullException(string message) : base(message) { }
    public FightNullException(string message, Exception inner) : base(message, inner) { }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client.
    protected FightNullException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}
