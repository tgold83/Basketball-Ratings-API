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
  public class PositionsController : ControllerBase
  {
    private readonly BasketballRatingsContext _db;

    public PositionsController(BasketballRatingsContext db)
    {
      _db = db;
    }

    // GET api/Positions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Position>>> Get(string positionName)
    {
      var query = _db.Positions.AsQueryable();

      if (positionName != null)
      {
        query = query.Where(entry => entry.PositionName == positionName);
      }

      return await query.ToListAsync();
    }

    // POST api/Positions
    [HttpPost]
    public async Task<ActionResult<Position>> Post(Position position)
    {
      _db.Positions.Add(position);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(Position), new { id = position.PositionId }, position);
    }

    // PUT: api/Positions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Position position)
    {
      if (id != position.PositionId)
      {
        return BadRequest();
      }

      _db.Entry(position).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PositionExists(id))
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
    private bool PositionExists(int id)
    {
      return _db.Positions.Any(e => e.PositionId == id);
    }

    // DELETE: api/Positions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePosition(int id)
    {
      var position = await _db.Positions.FindAsync(id);
      if (position == null)
      {
        return NotFound();
      }

      _db.Positions.Remove(position);
      await _db.SaveChangesAsync();

      return NoContent();
    } 
  }
}