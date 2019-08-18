
namespace Membership.Dtos.Accounts
{
   public class AccountDetailsDto
    {

        public int Id { get; set; }
        public bool IsValid { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
