using System.Text.Json.Serialization;

namespace BasketballRatings.Models
{
  public class PlayerPosition
  {
    public int PlayerPositionId { get; set; }
    public int PlayerId { get; set; }
    public int PositionId { get; set; }
    [JsonIgnore]
    public virtual Player Player { get; set; }
    [JsonIgnore]
    public virtual Position Position { get; set; }
  }
}