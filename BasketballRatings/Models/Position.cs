using System.Collections.Generic;

namespace BasketballRatings.Models
{
  public class Position
  {
    public Position()
    {
      this.JoinEntities = new HashSet<PlayerPosition>();
    }
    public int PositionId { get; set; }
    public string PositionName { get; set; }
    public virtual ICollection<PlayerPosition> JoinEntities { get; }
  }
}