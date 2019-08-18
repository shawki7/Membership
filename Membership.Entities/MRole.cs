using SharedMolecules.Entities;
using Microsoft.AspNetCore.Identity;

namespace Membership.Entities
{
    public class MRole : IdentityRole<int>, ISMEntity
    {
        #region Properties
        public bool IsValid { get; set; } = true;
        #endregion

        #region Constructors

        #endregion

        #region Methods
        #endregion
    }
}
