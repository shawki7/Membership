using Membership.SqlServer.Context;
using System;


namespace Membership.IData
{
    public abstract class BaseMembership : IDisposable
    {
        #region Properties
        public MembershipDbContext Context { get; set; }
        #endregion

        #region Constructors
        public BaseMembership(MembershipDbContext context)
        {
            Context = context;
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
        #endregion
    }
}
