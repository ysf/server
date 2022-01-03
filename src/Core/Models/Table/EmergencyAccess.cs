using System;
using System.ComponentModel.DataAnnotations;
using Bit.Core.Enums;
using Bit.Core.Utilities;

namespace Bit.Core.Models.Table
{
    public class EmergencyAccess : ITableObject<Guid>
    {
        public EmergencyAccess()
        {
        }

        public EmergencyAccess(Guid grantorId, string email, EmergencyAccessStatusType status,
            EmergencyAccessType type, int waitTimeDays, DateTime creationDate, DateTime revisionDate)
        {
            GrantorId = grantorId;
            Email = email;
            Status = status;
            Type = type;
            WaitTimeDays = waitTimeDays;
            CreationDate = creationDate;
            RevisionDate = revisionDate;
        }

        public EmergencyAccess(Guid grantorId, string email, EmergencyAccessStatusType status,
            EmergencyAccessType type, int waitTimeDays, DateTime creationDate, DateTime revisionDate,
            Guid id, Guid? granteeId, string keyEncrypted, DateTime? recoveryInitiatedDate, DateTime? lastNotificationDate)
            : this(grantorId, email, status, type, waitTimeDays, creationDate, revisionDate)
        {
            Id = id;
            GranteeId = granteeId;
            KeyEncrypted = keyEncrypted;
            RecoveryInitiatedDate = recoveryInitiatedDate;
            LastNotificationDate = lastNotificationDate;
        }

        public Guid Id { get; set; }
        public Guid GrantorId { get; private set; }
        public Guid? GranteeId { get; private set; }
        [MaxLength(256)]
        public string Email { get; private set; }
        public string KeyEncrypted { get; private set; }
        public EmergencyAccessType Type { get; private set; }
        public EmergencyAccessStatusType Status { get; private set; }
        public int WaitTimeDays { get; private set; }
        public DateTime? RecoveryInitiatedDate { get; private set; }
        public DateTime? LastNotificationDate { get; private set; }
        public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
        public DateTime RevisionDate { get; private set; } = DateTime.UtcNow;

        public void SetNewId()
        {
            Id = CoreHelpers.GenerateComb();
        }

        public EmergencyAccess ToEmergencyAccess()
        {
            return new EmergencyAccess(GrantorId, Email, Status, Type, WaitTimeDays, CreationDate, RevisionDate, Id,
                GranteeId, KeyEncrypted, RecoveryInitiatedDate, LastNotificationDate);
        }

        public void Accept(Guid granteeId)
        {
            Status = EmergencyAccessStatusType.Accepted;
            GranteeId = granteeId;
            Email = null;
        }

        public void Confirm(string key)
        {
            Status = EmergencyAccessStatusType.Confirmed;
            KeyEncrypted = key;
            Email = null;
        }

        public void InitiateRecovery()
        {
            Status = EmergencyAccessStatusType.RecoveryInitiated;
            RecoveryInitiatedDate = LastNotificationDate = RevisionDate = DateTime.UtcNow;
        }

        public void ApproveRecovery()
        {
            Status = EmergencyAccessStatusType.RecoveryApproved;
        }

        public void ConfirmRecovery()
        {
            Status = EmergencyAccessStatusType.Confirmed;
        }

        public void SentRecoveryNotification()
        {
            LastNotificationDate = DateTime.UtcNow;
        }

        public void Update(EmergencyAccessType type, int waitTimeDays, string keyEncrypted)
        {
            // Ensure we only set keys for a confirmed emergency access.
            if (!string.IsNullOrWhiteSpace(KeyEncrypted) && !string.IsNullOrWhiteSpace(keyEncrypted))
            {
                KeyEncrypted = keyEncrypted;
            }
            Type = type;
            WaitTimeDays = waitTimeDays;
        }
    }
}
