using System.ComponentModel.DataAnnotations;

namespace Mocktails.DAL.Model;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Category name")]
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 50 characters")]
    public string CategoryName { get; set; }
}
