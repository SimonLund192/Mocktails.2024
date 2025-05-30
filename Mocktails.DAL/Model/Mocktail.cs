﻿using System.ComponentModel.DataAnnotations;

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
    [Display(Name = "Quantity")]
    [Required]

    public int Quantity { get; set; }

    [Display(Name = "ImageUrl")]
    [Required]
    public string ImageUrl { get; set; }
    public byte[] RowVersion { get; set; }
}
