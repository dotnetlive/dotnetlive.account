using System;

namespace DotNetLive.Framework.UserIdentity.Models
{
    public class ConfirmationOccurrence : Occurrence
    {
        public ConfirmationOccurrence() : base()
        {
        }

        public ConfirmationOccurrence(DateTime confirmedOn) : base(confirmedOn)
        {
        }
    }
}
