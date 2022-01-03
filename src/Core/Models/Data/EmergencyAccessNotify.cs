using System;
using Bit.Core.Enums;
using Bit.Core.Models.Table;

namespace Bit.Core.Models.Data
{
    public class EmergencyAccessNotify : EmergencyAccess
    {
        public EmergencyAccessNotify(Guid id, Guid grantorId, Guid? granteeId, string email, string keyEncrypted,
            EmergencyAccessType type, EmergencyAccessStatusType status, int waitTimeDays,
            DateTime? recoveryInitiatedDate, DateTime? lastNotificationDate, DateTime creationDate,
            DateTime revisionDate, string granteeName, string granteeEmail, string grantorEmail) : base(grantorId, email, status, type, waitTimeDays, creationDate, revisionDate, id, granteeId, keyEncrypted, recoveryInitiatedDate, lastNotificationDate)
        {
            GranteeName = granteeName;
            GranteeEmail = granteeEmail;
            GrantorEmail = grantorEmail;
        }

        public string GrantorEmail { get; }
        public string GranteeName { get; }
        public string GranteeEmail { get; }
    }
}
