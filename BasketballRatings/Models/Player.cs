using System.Collections.Generic;

namespace BasketballRatings.Models
{
  public class Player
  {
    public Player()
    {
      this.JoinEntities = new HashSet<PlayerPosition>();
    }
    
    public int PlayerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Team { get; set; }
    public virtual ICollection<PlayerPosition> JoinEntities { get; set; }
  }
}