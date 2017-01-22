using System;

namespace stubbl
{
    internal class CurrentTeamNotSetException : Exception
    {
        public CurrentTeamNotSetException()
        {
        }

        public CurrentTeamNotSetException(string message) : base(message)
        {
        }

        public CurrentTeamNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}