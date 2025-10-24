using Microsoft.AspNetCore.Identity;
using System;

namespace ProductManagementSystem.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}