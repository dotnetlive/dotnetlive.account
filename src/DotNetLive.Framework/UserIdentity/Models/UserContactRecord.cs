using System;

namespace DotNetLive.Framework.UserIdentity.Models
{
    public abstract class UserContactRecord : IEquatable<UserEmail>
    {
        protected UserContactRecord(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        public string Value { get; private set; }
        public ConfirmationOccurrence ConfirmationRecord { get; private set; }

        public bool IsConfirmed()
        {
            return ConfirmationRecord != null;
        }

        public void SetConfirmed()
        {
            SetConfirmed(new ConfirmationOccurrence());
        }

        public void SetConfirmed(ConfirmationOccurrence confirmationRecord)
        {
            if (ConfirmationRecord == null)
            {
                ConfirmationRecord = confirmationRecord;
            }
        }

        public void SetUnconfirmed()
        {
            ConfirmationRecord = null;
        }

        public bool Equals(UserEmail other)
        {
            return other.Value.Equals(Value);
        }
    }
}
