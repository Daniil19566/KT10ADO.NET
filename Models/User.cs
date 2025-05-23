using System.ComponentModel.DataAnnotations;
using KT10ADO.NET.Models;
using Microsoft.Identity.Client;

namespace KT10ADO.NET.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public UserProfile Profile { get; set; }
    }
}