using Membership.Entities;
using Microsoft.EntityFrameworkCore;
using SharedMolecules.EFCore.Context;

namespace Membership.SqlServer.Context
{
   public class MembershipDbContext : SMContext<MUser, MRole, int
           , MUserClaim, MUserRole, MUserLogin
           , MRoleClaim, MUserToken>
    {
        #region Properties

        #endregion

        #region Constructors
        public MembershipDbContext(DbContextOptions options) : base(options)
        {
        }
        #endregion


        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}