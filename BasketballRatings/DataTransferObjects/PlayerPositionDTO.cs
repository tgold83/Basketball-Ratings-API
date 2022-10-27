using System.Collections.Generic;

namespace DTO
{
  public class PlayerPositionDTO
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> PositionName { get; set; }
    public string Team { get; set; }
    public int PlayerId { get; set; }
  }
}