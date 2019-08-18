using Microsoft.AspNetCore.Identity;
using SharedMolecules.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Membership.Entities
{
    public class MRoleClaim : IdentityRoleClaim<int>, ISMEntity
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
