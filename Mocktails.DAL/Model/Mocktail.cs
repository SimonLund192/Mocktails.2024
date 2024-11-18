using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.Model;
public class Mocktail
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Mocktail name")]
    [Required(ErrorMessage = "Mocktail name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Mocktail name must be between 3 and 50 characters")]
    public string Name { get; set; }
    [Display(Name = "Description")]
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 100 characters")]
    public string Description { get; set; }
    [Display(Name = "Price")]
    [Required]
    public decimal Price { get; set; }
    [Display(Name = "ImageUrl")]
    [Required]
    public string ImageUrl { get; set; }
}
