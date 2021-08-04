using System;
using Bit.Core.Enums.Provider;

namespace Bit.Core.Models.Business.Provider
{
    public interface IConsortium
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }

    public interface IConsortiumAssociation
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Email { get; set; }
        public Enums.AssociationStatusType Status { get; set; }
    }

    public interface IInvite<T1, T2, T3>
    {
        T1 GroupIdentifier { get; set; }
        T2 InviterIdentifier { get; set; }
        T3 InviteeIdentifier { get; set; }
    }
    
    public interface IOriginalInvite<T1, T2, T3, T4>: IInvite<T1, T2, T3>
    {
        T4 InviteeRoleType { get; set; }
    }

    public class ProviderUserInvite : IOriginalInvite<Guid, Guid, string, ProviderUserType>
    {
        public Guid GroupIdentifier { get; set; }
        public Guid InviterIdentifier { get; set; }
        public string InviteeIdentifier { get; set; }
        public ProviderUserType InviteeRoleType { get; set; }

        public ProviderUserInvite(Guid providerId, Guid inviterUserId, string inviteeEmail, ProviderUserType inviteeRole)
        {
            GroupIdentifier = providerId;
            InviterIdentifier = inviterUserId;
            InviteeIdentifier = inviteeEmail;
            InviteeRoleType = inviteeRole;
        }
    }

    public class ProviderUserInviteResend : IInvite<Guid, Guid, Guid>
    {
        public Guid GroupIdentifier { get; set; }
        public Guid InviterIdentifier { get; set; }
        public Guid InviteeIdentifier { get; set; }

        public ProviderUserInviteResend(Guid providerId, Guid inviterUserId, Guid inviteeUserId)
        {
            GroupIdentifier = providerId;
            InviterIdentifier = inviterUserId;
            InviteeIdentifier = inviteeUserId;
        }
    }

    public class ProviderSetupInviteResend: IInvite<Guid, string, Guid>
    {
        public Guid GroupIdentifier { get; set; }
        public string InviterIdentifier { get; set; }
        public Guid InviteeIdentifier { get; set; }

        public ProviderSetupInviteResend(Guid providerId, string inviterEmail, Guid inviteeUserId)
        {
            GroupIdentifier = providerId;
            InviterIdentifier = inviterEmail;
            InviteeIdentifier = inviteeUserId;
        }
    }
}
