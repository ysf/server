using System;
using Bit.Core.Enums;
using Bit.Core.Models.Table;

namespace Bit.Core.Models.Data
{
    public class EmergencyAccessDetails : EmergencyAccess
    {
        public EmergencyAccessDetails(Guid id, Guid grantorId, Guid? granteeId, string email, string keyEncrypted,
            EmergencyAccessType type, EmergencyAccessStatusType status, int waitTimeDays,
            DateTime? recoveryInitiatedDate, DateTime? lastNotificationDate, DateTime creationDate,
            DateTime revisionDate, string granteeName, string granteeEmail, string grantorName, string grantorEmail)
        : base(grantorId, email, status, type, waitTimeDays, creationDate, revisionDate, id, granteeId, keyEncrypted, recoveryInitiatedDate, lastNotificationDate)
        {
            GranteeName = granteeName;
            GranteeEmail = granteeEmail;
            GrantorName = grantorName;
            GrantorEmail = grantorEmail;
        }

        public string GranteeName { get; }
        public string GranteeEmail { get; }
        public string GrantorName { get; }
        public string GrantorEmail { get; }
    }
}
