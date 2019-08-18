using Membership.Dtos.Accounts;
using Membership.IData;
using Membership.IData.Repositories;
using Membership.SqlServer.Context;
using SharedMolecules.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Membership.Data.Repositories
{
    public class UsersRepository : BaseMembership, IUsersRepository
    {
        #region Properties

        #endregion


        #region Constructor
        public UsersRepository(MembershipDbContext context) : base(context)
        {
        }

        #endregion

        #region Methods

        public IEnumerable<BasicUserDetailsDto> GetAllUsers()
        {
            return Context.Users
                 .Where(user => user.IsValid)
                 .Select(user => new BasicUserDetailsDto() { Id = user.Id, FullName = user.FirstName + user.LastName })
                 .ToList();
        }

        public AccountDetailsDto GetAccount(int id)
        {
            return Context.Users
                .Select(user => new AccountDetailsDto()
                {
                    Id = user.Id,
                    IsValid = user.IsValid,
                    FullName = user.FirstName + user.LastName,
                    UserName = user.UserName
                }).SingleOrDefault(user => user.IsValid && user.Id == id);
        }

        #endregion

    }
}
