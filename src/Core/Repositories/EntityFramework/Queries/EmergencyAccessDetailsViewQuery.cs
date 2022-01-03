using System.Linq;
using Bit.Core.Models.Data;

namespace Bit.Core.Repositories.EntityFramework.Queries
{
    public class EmergencyAccessDetailsViewQuery : IQuery<EmergencyAccessDetails>
    {
        public IQueryable<EmergencyAccessDetails> Run(DatabaseContext dbContext)
        {
            var query = from ea in dbContext.EmergencyAccesses
                        join grantee in dbContext.Users
                            on ea.GranteeId equals grantee.Id into grantee_g
                        from grantee in grantee_g.DefaultIfEmpty()
                        join grantor in dbContext.Users
                            on ea.GrantorId equals grantor.Id into grantor_g
                        from grantor in grantor_g.DefaultIfEmpty()
                        select new { ea, grantee, grantor };
            return query.Select(x => new EmergencyAccessDetails(x.ea.Id, x.ea.GrantorId,
                x.ea.GranteeId, x.ea.Email, x.ea.KeyEncrypted, x.ea.Type,
                x.ea.Status, x.ea.WaitTimeDays, x.ea.RecoveryInitiatedDate,
                x.ea.LastNotificationDate, x.ea.CreationDate,
                x.ea.RevisionDate, x.grantee.Name, x.grantee.Email,
                x.grantor.Name, x.grantor.Email));
        }
    }
}
