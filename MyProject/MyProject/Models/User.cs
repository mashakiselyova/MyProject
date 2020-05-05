using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyProject.Models
{
    public class User : IdentityUser
    {
        public byte[] Avatar { get; set; }
        public List<Collection> Collections { get; set; }
    }

}
