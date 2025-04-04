using System.ComponentModel.DataAnnotations;

namespace KT10ADO.NET
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public UserProfile Profile { get; set; }
    }
}
