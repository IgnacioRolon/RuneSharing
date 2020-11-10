using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharing
{
    [Serializable]
    public class LeagueNotRunningException : Exception
    {
        public LeagueNotRunningException() { }
        public LeagueNotRunningException(string message) : base(message) { }
        public LeagueNotRunningException(string message, Exception inner) : base(message, inner) { }
        protected LeagueNotRunningException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class InvalidRuneSetException : Exception
    {
        public InvalidRuneSetException() { }
        public InvalidRuneSetException(string message) : base(message) { }
        public InvalidRuneSetException(string message, Exception inner) : base(message, inner) { }
        protected InvalidRuneSetException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
