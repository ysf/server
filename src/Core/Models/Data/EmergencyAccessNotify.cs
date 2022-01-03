using Bit.Core.Models.Table;

namespace Bit.Core.Models.Data
{
    public record EmergencyAccessNotify : EmergencyAccess
    {
        public string GrantorEmail { get; init; }
        public string GranteeName { get; init; }
        public string GranteeEmail { get; init; }
    }
}
