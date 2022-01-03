using System.ComponentModel.DataAnnotations;
using Bit.Core.Enums;
using Bit.Core.Models.Table;
using Bit.Core.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bit.Api.Models.Request
{
    public class EmergencyAccessInviteRequestModel
    {
        [Required]
        [StrictEmailAddress]
        [StringLength(256)]
        public string Email { get; set; }
        [Required]
        public EmergencyAccessType? Type { get; set; }
        [Required]
        public int WaitTimeDays { get; set; }
    }

    public class EmergencyAccessUpdateRequestModel
    {
        [Required]
        public EmergencyAccessType Type { get; set; }
        [Required]
        public int WaitTimeDays { get; set; }
        public string KeyEncrypted { get; set; }

        public EmergencyAccess ToEmergencyAccess(EmergencyAccess existingEmergencyAccess)
        {
            existingEmergencyAccess.Update(Type, WaitTimeDays, KeyEncrypted);
            
            return existingEmergencyAccess;
        }
    }

    public class EmergencyAccessPasswordRequestModel
    {
        [Required]
        [StringLength(300)]
        public string NewMasterPasswordHash { get; set; }
        [Required]
        public string Key { get; set; }
    }
}
