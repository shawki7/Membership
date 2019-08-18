using Membership.Dtos.Accounts;
using SharedMolecules.Dto;
using System.Collections.Generic;

namespace Membership.IData.Repositories
{
   public interface IUsersRepository 
    {
       IEnumerable<BasicUserDetailsDto> GetAllUsers();
       AccountDetailsDto GetAccount(int id);
    }
}