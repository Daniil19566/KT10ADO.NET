using System.ComponentModel.DataAnnotations;

namespace KT10ADO.NET.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public string Bio { get; set; }
        public string UserProfileEmail { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

}