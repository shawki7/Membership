using Membership.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Membership.Dtos.Authentication
{
    public class AuthDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
    }
}
