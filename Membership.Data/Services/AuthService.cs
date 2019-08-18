using Membership.Dtos.Authentication;
using Membership.Entities;
using Membership.Entities.Enums;
using Membership.IData;
using Membership.IData.Services;
using Membership.SqlServer.Context;
using Microsoft.AspNetCore.Identity;
using SharedMolecules.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Membership.Data.Services
{
    public class AuthService : BaseMembership, IAuthService 
    {
        #region Properties
        public SignInManager<MUser> SignInManager { get; set; }
        public UserManager<MUser> UserManager { get; set; }
        #endregion

        #region Constructor
        public AuthService(MembershipDbContext context
          , SignInManager<MUser> signInManager,
         UserManager<MUser> userManager) : base(context)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }
        #endregion

        #region Methods

        public SMOperationResult<AuthDto,SMResult> SignUp(SignUpDto signUpDto)
        {
            try
            {
                var user = new MUser()
                {
                    UserName = signUpDto.Username,
                    FirstName = signUpDto.FirstName,
                    LastName = signUpDto.LastName,
                    Email = signUpDto.Email,
                    EmailConfirmed = true,
                    PhoneNumber = signUpDto.PhoneNumber
                };

                var operationResult = UserManager.CreateAsync(user, signUpDto.Password);
                if (!operationResult.Result.Succeeded)
                {
                    return new SMOperationResult<AuthDto, SMResult>(null, SMResult.Failed);
                }

                else
                {
                    operationResult =  UserManager.AddToRoleAsync(user, Role.User.ToString());
                    if (!operationResult.Result.Succeeded)
                    {
                         UserManager.DeleteAsync(user);
                        var result = new SMOperationResult<AuthDto, SMResult>(null, SMResult.Failed);
                    
                        return result;
                    }
                }
                var authDto = new AuthDto()
                {
                    Username = user.UserName,
                    Id = user.Id,
                    Roles = new List<Role>() { Role.User }
                };

                return new SMOperationResult<AuthDto, SMResult>(authDto, SMResult.Success);
            }
            catch (Exception ex)
            {
                return new SMOperationResult<AuthDto, SMResult>(null, SMResult.Failed);
            }
        }

        public SMOperationResult<AuthDto,SMResult> Login(LoginDto loginDto)
        {
            var userEntity = GetUser(loginDto.Username);
            if (userEntity == null)
            {
                return null;
            }
            var result =  SignInManager.CheckPasswordSignInAsync
                (userEntity, loginDto.Password, false);
            var roles = (UserManager.GetRolesAsync(userEntity));

            var authDto = new AuthDto() {
                Id = userEntity.Id, Roles = roles.Result
                .Select(role => (Role)Enum.Parse(typeof(Role), role))
                .ToList(),
                Username = userEntity.UserName
            };
           

            if (result.Result.Succeeded)
            {
                return new SMOperationResult<AuthDto, SMResult>(authDto, SMResult.Success);
            }

            return null;
        }

        public async Task<SMOperationResult<SMResult>> Logout()
        {
            await SignInManager.SignOutAsync();
            return new SMOperationResult<SMResult>(SMResult.Success);
        }

        public bool IsEmailExists(string email)
        {
            return Context.Users.Any(user => user.Email == email);
        }

        public bool IsUsernameExists(string username)
        {
            return Context.Users.Any(user => user.UserName == username);
        }

        #region Private Methods
        private MUser GetUser(string username) => Context.Users
           .Where(user => user.IsValid && user.UserName == username)
           .SingleOrDefault();
        #endregion

        #endregion

    }
}
