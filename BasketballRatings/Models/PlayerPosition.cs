namespace BasketballRatings.Models
{
  public class PlayerPosition
  {
    public int PlayerPositionId { get; set; }
    public int PlayerId { get; set; }
    public int PositionId { get; set; }
    public virtual Player Player { get; set; }
    public virtual Position Position { get; set; }
  }
}