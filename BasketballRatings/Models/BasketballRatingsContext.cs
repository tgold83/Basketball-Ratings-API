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
              new Player { PlayerId = 5, FirstName = "Nikola", LastName = "Jokic", Team = "DEN"},
              new Player { PlayerId = 6, FirstName = "Joel", LastName = "Embiid", Team = "PHI"},
              new Player { PlayerId = 7, FirstName = "Luka", LastName = "Doncic", Team = "DAL"},
              new Player { PlayerId = 8, FirstName = "Kawhi", LastName = "Leonard", Team = "LAC"},
              new Player { PlayerId = 9, FirstName = "Ja", LastName = "Morant", Team = "MEM"},
              new Player { PlayerId = 10, FirstName = "Jason", LastName = "Tatum", Team = "BOS"}
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
            new PlayerPosition { PlayerPositionId = 1, PlayerId = 1, PositionId = 1},
            new PlayerPosition { PlayerPositionId = 2, PlayerId = 1, PositionId = 3},
            new PlayerPosition { PlayerPositionId = 3, PlayerId = 2, PositionId = 4},
            new PlayerPosition { PlayerPositionId = 4, PlayerId = 2, PositionId = 5},
            new PlayerPosition { PlayerPositionId = 5, PlayerId = 3, PositionId = 1},
            new PlayerPosition { PlayerPositionId = 6, PlayerId = 3, PositionId = 2},
            new PlayerPosition { PlayerPositionId = 7, PlayerId = 4, PositionId = 3},
            new PlayerPosition { PlayerPositionId = 8, PlayerId = 4, PositionId = 4},
            new PlayerPosition { PlayerPositionId = 9, PlayerId = 5, PositionId = 5},
            new PlayerPosition { PlayerPositionId = 10, PlayerId = 6, PositionId = 5},
            new PlayerPosition { PlayerPositionId = 11, PlayerId = 7, PositionId = 1},
            new PlayerPosition { PlayerPositionId = 12, PlayerId = 7, PositionId = 3},
            new PlayerPosition { PlayerPositionId = 13, PlayerId = 8, PositionId = 3},
            new PlayerPosition { PlayerPositionId = 14, PlayerId = 8, PositionId = 4},
            new PlayerPosition { PlayerPositionId = 15, PlayerId = 9, PositionId = 1},
            new PlayerPosition { PlayerPositionId = 16, PlayerId = 10, PositionId = 3},
            new PlayerPosition { PlayerPositionId = 17, PlayerId = 10, PositionId = 4}
        );
    }
  }
}