using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Finances.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]        
        public string Title { get; set; } = string.Empty;
        [Required] 
        public string Description { get; set; } = string.Empty;
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public string Author { get; set;} = string.Empty;

        [ValidateNever] 
        public string ImageUrl { get; set; } = string.Empty;

    }
}
