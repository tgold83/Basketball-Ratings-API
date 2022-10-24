using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasketballRatings.Models;
using System.Linq;

namespace BasketballRatings.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlayersController : ControllerBase
  {
    private readonly BasketballRatingsContext _db;

    public PlayersController(BasketballRatingsContext db)
    {
      _db = db;
    }

    // GET api/players
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> Get(string firstName, string lastName, string team)
    {
      var query = _db.Players.AsQueryable();

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

      return await query.ToListAsync();
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