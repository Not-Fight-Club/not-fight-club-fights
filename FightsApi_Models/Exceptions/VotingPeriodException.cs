using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Models.Exceptions
{
  public class VotingPeriodException : Exception
  {
    public VotingPeriodException() : base() { }
    public VotingPeriodException(string message) : base(message) { }
    public VotingPeriodException(string message, Exception inner) : base(message, inner) { }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client.
    protected VotingPeriodException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}
