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
  public class PositionsController : ControllerBase
  {
    private readonly IJWTManagerRepository _jWTManager;
    private readonly BasketballRatingsContext _db;

	  public PositionsController(IJWTManagerRepository jWTManager, BasketballRatingsContext db)
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

    // GET api/Positions
    [HttpGet]
    public ActionResult<List<PositionPlayerDTO>> Get (string positionName)
    {
      List<PositionPlayerDTO> positionPlayer = new List<PositionPlayerDTO>(){};

      var query = _db.Positions.Include(position => position.JoinEntities).ThenInclude(join => join.Player).AsQueryable();

      if (positionName != null)
      {
        query = query.Where(entry => entry.PositionName == positionName);
      }

      var positionList = query.ToList();
      foreach(Position position in positionList)
      {
        var PositionPlayerDTO = new PositionPlayerDTO() { PositionName = position.PositionName, PositionId = position.PositionId}; 
        var PositionPlayerList = new List<string>(){};
        foreach(PlayerPosition join in position.JoinEntities)
        {
          var playerName = join.Player.FirstName + " " + join.Player.LastName;
          PositionPlayerList.Add(playerName);
        }
        PositionPlayerDTO.PlayerName = PositionPlayerList;
        positionPlayer.Add(PositionPlayerDTO);
      }

      return positionPlayer;
    }

    // GET: api/Positions/5
    [HttpGet("{id}")]
    public ActionResult<PositionPlayerDTO> Get(int id)
    {
      var position = _db.Positions.Include(position => position.JoinEntities).ThenInclude(join => join.Player).FirstOrDefault(entry => entry.PositionId == id);
      var PositionPlayerDTO = new PositionPlayerDTO() { PositionName = position.PositionName, PositionId = position.PositionId}; 
      var PositionPlayerList = new List<string>(){};
      foreach(PlayerPosition join in position.JoinEntities)
      {
        var playerName = join.Player.FirstName + " " + join.Player.LastName;
        PositionPlayerList.Add(playerName);
      }
      PositionPlayerDTO.PlayerName = PositionPlayerList;
      return PositionPlayerDTO;
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