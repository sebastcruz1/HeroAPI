using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeroAPI.Data;
using HeroAPI.Models;

namespace HeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HeroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Hero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Heros>>> GetHero()
        {
            return await _context.Hero.ToListAsync();
        }

        // GET: api/Hero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Heros>> GetHeros(int id)
        {
            var heros = await _context.Hero.FindAsync(id);

            if (heros == null)
            {
                return NotFound();
            }

            return heros;
        }

        // PUT: api/Hero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeros(int id, Heros heros)
        {
            if (id != heros.Id)
            {
                return BadRequest();
            }

            _context.Entry(heros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HerosExists(id))
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

        // POST: api/Hero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Heros>> PostHeros(Heros heros)
        {
            _context.Hero.Add(heros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHeros", new { id = heros.Id }, heros);
        }

        // DELETE: api/Hero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeros(int id)
        {
            var heros = await _context.Hero.FindAsync(id);
            if (heros == null)
            {
                return NotFound();
            }

            _context.Hero.Remove(heros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HerosExists(int id)
        {
            return _context.Hero.Any(e => e.Id == id);
        }
    }
}
