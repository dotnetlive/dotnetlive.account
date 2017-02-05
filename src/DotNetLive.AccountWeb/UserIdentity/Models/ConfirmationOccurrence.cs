using System;

namespace DotNetLive.AccountWeb.UserIdentity.Models
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
