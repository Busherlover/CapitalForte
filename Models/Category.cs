using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Finances.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Display order")]
        [Range(1,100)]
        public int DisplayOrder { get; set; }
    }
}
