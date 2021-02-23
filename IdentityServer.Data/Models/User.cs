using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Balance { get; set; }

    }
}