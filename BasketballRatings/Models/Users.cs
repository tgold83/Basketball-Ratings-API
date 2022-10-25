using System.ComponentModel.DataAnnotations;

namespace BasketballRatings.Models
{
  public class Users
  {
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Password { get; set; }
  }
}