#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace Foods.Models;

public class Food
{
    [Key]
    public int FoodId {get; set;}
    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string Dish {get; set;}
    [Required]
    [MinLength(2)]
    public string Chef {get; set;}
    public int Tasty {get; set;}
    public int Cals {get; set;}
    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string Description {get; set;}

    public DateTime CreatedAt {get; set;} =DateTime.Now;
    public DateTime UpdatedAt {get; set;} =DateTime.Now;
}