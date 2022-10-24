using Microsoft.EntityFrameworkCore;

namespace BasketballRatings.Models
{
  public class BasketballRatingsContext : DbContext
  {
    public BasketballRatingsContext(DbContextOptions<BasketballRatingsContext> options)
      : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<PlayerPosition> PlayerPosition { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Player>()
          .HasData(
              new Player { PlayerId = 1, FirstName = "LeBron", LastName = "James", Team = "LAL"},
              new Player { PlayerId = 2, FirstName = "Giannis", LastName = "Antetokoumpo", Team = "MIL"},
              new Player { PlayerId = 3, FirstName = "Stephen", LastName = "Curry", Team = "GSW"},
              new Player { PlayerId = 4, FirstName = "Kevin", LastName = "Durant", Team = "BKN"},
              new Player { PlayerId = 5, FirstName = "Nikola", LastName = "Jokic", Team = "DEN"}
          );
      
      builder.Entity<Position>()
          .HasData(
              new Position { PositionId = 1, PositionName = "PG"},
              new Position { PositionId = 2, PositionName = "SG"},
              new Position { PositionId = 3, PositionName = "SF"},
              new Position { PositionId = 4, PositionName = "PF"},
              new Position { PositionId = 5, PositionName = "C"}
          );

      builder.Entity<PlayerPosition>()
        .HasData(
            new PlayerPosition { PlayerId = 1, PositionId = 1},
            new PlayerPosition { PlayerId = 1, PositionId = 3}
        );
    }
  }
}