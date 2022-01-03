using Bit.Core.Models.Table;

namespace Bit.Core.Models.Data
{
    public record EmergencyAccessDetails : EmergencyAccess
    {
        public string GranteeName { get; init; }
        public string GranteeEmail { get; init; }
        public string GrantorName { get; init; }
        public string GrantorEmail { get; init; }
    }
}
