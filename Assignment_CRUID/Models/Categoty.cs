using System.ComponentModel.DataAnnotations;

namespace Assignment_CRUID.Models
{
    public class Categoty
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
