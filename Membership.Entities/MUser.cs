using Microsoft.AspNetCore.Identity;
using SharedMolecules.Entities;


namespace Membership.Entities
{
   public class MUser : IdentityUser<int>, ISMEntity
    {
        #region Properties
        public bool IsValid { get; set; } = true;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        #endregion


        #region Constructor


        #endregion

        #region Methods

        #endregion

    }
}
