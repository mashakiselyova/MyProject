using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyProject.Models
{
    public class User : IdentityUser
    {
        public byte[] Avatar { get; set; }
        public List<Vocabulary> Vocabularies { get; set; }

        public User()
        {
            Vocabularies = new List<Vocabulary>();
        }
    }

}
