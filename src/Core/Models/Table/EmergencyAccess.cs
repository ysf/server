using System;
using System.ComponentModel.DataAnnotations;
using Bit.Core.Enums;
using Bit.Core.Utilities;

namespace Bit.Core.Models.Table
{
    public record EmergencyAccess : ITableObject<Guid>
    {
        public EmergencyAccess()
        {
        }

        public Guid Id { get; set; }
        public Guid GrantorId { get; init; }
        public Guid? GranteeId { get; init; }
        [MaxLength(256)]
        public string Email { get; init; }
        public string KeyEncrypted { get; init; }
        public EmergencyAccessType Type { get; init; }
        public EmergencyAccessStatusType Status { get; init; }
        public int WaitTimeDays { get; init; }
        public DateTime? RecoveryInitiatedDate { get; init; }
        public DateTime? LastNotificationDate { get; init; }
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
        public DateTime RevisionDate { get; init; } = DateTime.UtcNow;

        public void SetNewId()
        {
            Id = CoreHelpers.GenerateComb();
        }

        public EmergencyAccess ToEmergencyAccess()
        {
            return this with { };
        }

        public EmergencyAccess Accept(Guid granteeId)
        {
            return this with
            {
                Status = EmergencyAccessStatusType.Accepted,
                GranteeId = granteeId,
                Email = null,
            };
        }

        public EmergencyAccess Confirm(string key)
        {
            return this with
            {
                Status = EmergencyAccessStatusType.Confirmed,
                KeyEncrypted = key,
                Email = null,
            };
        }

        public EmergencyAccess InitiateRecovery()
        {
            var now = DateTime.Now;
            return this with
            {
                Status = EmergencyAccessStatusType.RecoveryInitiated,
                RecoveryInitiatedDate = now,
                LastNotificationDate = now,
                RevisionDate = now,
            };
        }

        public EmergencyAccess ApproveRecovery()
        {
            return this with { Status = EmergencyAccessStatusType.RecoveryApproved };
        }

        public EmergencyAccess ConfirmRecovery()
        {
            return this with { Status = EmergencyAccessStatusType.Confirmed };
        }

        public EmergencyAccess SentRecoveryNotification()
        {
            return this with { LastNotificationDate = DateTime.UtcNow };
        }

        public EmergencyAccess Update(EmergencyAccessType type, int waitTimeDays, string keyEncrypted)
        {
            // Ensure we only set keys for a confirmed emergency access.
            var ke = KeyEncrypted;
            if (!string.IsNullOrWhiteSpace(KeyEncrypted) && !string.IsNullOrWhiteSpace(keyEncrypted))
            {
                ke = keyEncrypted;
            }

            return this with { KeyEncrypted = ke, Type = type, WaitTimeDays = waitTimeDays, };
        }
    }
}
