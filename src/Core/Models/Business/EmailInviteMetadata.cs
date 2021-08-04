using System;

namespace Bit.Core.Models.Business
{
    public class EmailInviteMetadata
    {
        public bool IsResend { get; set; }
        public Guid? InvitingUserId { get; set; }
    }
}
