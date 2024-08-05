using System.ComponentModel.DataAnnotations;
namespace Finances.Models
{
    public class UserInRole
    {
        [Key]
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
