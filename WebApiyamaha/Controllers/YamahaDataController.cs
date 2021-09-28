using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiyamaha.Models;

namespace WebApiyamaha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YamahaDataController : ControllerBase
    {
        private readonly Context _context;

        public YamahaDataController(Context context)
        {
            _context = context;
        }

        // GET: api/YamahaData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YamahaData>>> GetYamahaData()
        {
            return await _context.YamahaData.ToListAsync();
        }

        // GET: api/YamahaData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YamahaData>> GetYamahaData(int id)
        {
            var yamahaData = await _context.YamahaData.FindAsync(id);

            if (yamahaData == null)
            {
                return NotFound();
            }

            return yamahaData;
        }

        // PUT: api/YamahaData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYamahaData(int id, YamahaData yamahaData)
        {
            if (id != yamahaData.Id)
            {
                return BadRequest();
            }

            _context.Entry(yamahaData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YamahaDataExists(id))
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

        // POST: api/YamahaData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<YamahaData>> PostYamahaData(YamahaData yamahaData)
        {
            _context.YamahaData.Add(yamahaData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetYamahaData), new { id = yamahaData.Id }, yamahaData);
        }

        // DELETE: api/YamahaData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYamahaData(int id)
        {
            var yamahaData = await _context.YamahaData.FindAsync(id);
            if (yamahaData == null)
            {
                return NotFound();
            }

            _context.YamahaData.Remove(yamahaData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YamahaDataExists(int id)
        {
            return _context.YamahaData.Any(e => e.Id == id);
        }
    }
}
