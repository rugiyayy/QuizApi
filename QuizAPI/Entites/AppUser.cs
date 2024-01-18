using Microsoft.AspNetCore.Identity;

namespace QuizAPI.Entites
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
