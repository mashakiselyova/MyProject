﻿using Microsoft.AspNetCore.Identity;

namespace NewsApp.Models
{
    public class User : IdentityUser
    {
        public byte[] Avatar { get; set; }
    }
}
