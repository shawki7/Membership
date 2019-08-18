using Membership.Dtos.Authentication;
using SharedMolecules.Core.OperationResult;
using SharedMolecules.Dto;
using System.Threading.Tasks;

namespace Membership.IData.Services
{
   public interface IAuthService
    {
        SMOperationResult<AuthDto, SMResult> SignUp(SignUpDto signUpDto);
        SMOperationResult<AuthDto, SMResult> Login(LoginDto loginDto);
        Task<SMOperationResult<SMResult>> Logout();
        bool IsEmailExists(string email);
        bool IsUsernameExists(string username);
    }
}
