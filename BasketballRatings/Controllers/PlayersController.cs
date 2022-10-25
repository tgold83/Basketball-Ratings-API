using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasketballRatings.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BasketballRatings.Repository;

namespace BasketballRatings.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class PlayersController : ControllerBase
  {
    private readonly IJWTManagerRepository _jWTManager;
    private readonly BasketballRatingsContext _db;

	  public PlayersController(IJWTManagerRepository jWTManager, BasketballRatingsContext db)
    {
      this._jWTManager = jWTManager;
      _db = db;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    public IActionResult Authenticate(Users usersdata)
    {
      var token = _jWTManager.Authenticate(usersdata);

      if (token == null)
      {
        return Unauthorized();
      }

      return Ok(token);
    }

    

    // GET api/players
    [HttpGet]
    public ActionResult<List<PlayerPositionDTO>> Get (string firstName, string lastName, string team)
    {
      List<PlayerPositionDTO> playerPosition = new List<PlayerPositionDTO>(){};

      var query = _db.Players.Include(player => player.JoinEntities).ThenInclude(join => join.Position).AsQueryable();

      if (firstName != null)
      {
        query = query.Where(entry => entry.FirstName == firstName);
      }

      if (lastName != null)
      {
        query = query.Where(entry => entry.LastName == lastName);
      }

      if (team != null)
      {
        query = query.Where(entry => entry.Team == team);
      }
      
      var playerList = query.ToList();
      foreach(Player player in playerList)
      {
        var PlayerPositionDTO = new PlayerPositionDTO() { FirstName = player.FirstName , LastName = player.LastName, Team = player.Team}; 
        var PlayerPositionList = new List<string>(){};
        foreach(PlayerPosition join in player.JoinEntities)
        {
          var position = join.Position.PositionName;
          PlayerPositionList.Add(position);
        }
        PlayerPositionDTO.PositionName = PlayerPositionList;
        playerPosition.Add(PlayerPositionDTO);
      }

      return playerPosition;
    }

    // POST api/players
    [HttpPost]
    public async Task<ActionResult<Player>> Post(Player player)
    {
      _db.Players.Add(player);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(Player), new { id = player.PlayerId }, player);
    }

    // PUT: api/Players/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Player player)
    {
      if (id != player.PlayerId)
      {
        return BadRequest();
      }

      _db.Entry(player).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PlayerExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }
    private bool PlayerExists(int id)
    {
      return _db.Players.Any(e => e.PlayerId == id);
    }

    // DELETE: api/Players/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
      var player = await _db.Players.FindAsync(id);
      if (player == null)
      {
        return NotFound();
      }

      _db.Players.Remove(player);
      await _db.SaveChangesAsync();

      return NoContent();
    } 
  }
}