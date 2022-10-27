using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    { 
        
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;

        }
        

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("(Id)")]
        public async Task<ActionResult<SuperHero>> Get(int Id)
        {
            var hero = await _context.SuperHeroes.FindAsync(Id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = _context.SuperHeroes.Find(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            dbHero.Name=request.Name;
            dbHero.FirstName=request.FirstName;
            dbHero.LastName=request.LastName;
            dbHero.Place=request.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("(Id)")]
        public async Task<ActionResult<SuperHero>> Delete(int Id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }
}
