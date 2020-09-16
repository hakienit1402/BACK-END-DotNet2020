using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Dotnet2020.Models;

namespace Shop_Dotnet2020.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class SanphamsController : ControllerBase
    {
        ShopDotnetContext _context = new ShopDotnetContext();
        [HttpGet("top")]
        public async Task<ActionResult<IEnumerable<Sanpham>>> Top()
        {

            var sp = _context.Sanphams
                                .FromSqlRaw("SELECT TOP 10  * FROM Sanpham order by Idsp desc;")
                                .ToList();
            return sp;
        }

        // GET: api/Sanphams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sanpham>>> GetSanphams()
        {
            return await _context.Sanphams.Include(hd => hd.Feedbacks).ToListAsync();
        }

        // GET: api/Sanphams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sanpham>> GetSanpham(int id)
        {
            var sanpham = await _context.Sanphams.Include(hd => hd.Feedbacks).Where(hd => hd.Idsp == id)
                .FirstOrDefaultAsync();

            if (sanpham == null)
            {
                return NotFound();
            }

            return sanpham;
        }
        [HttpGet("th/{idth}")]
        public async Task<ActionResult<IEnumerable<Sanpham>>> GetSanphamTH(int idth)
        {
            var sanpham =  _context.Sanphams.Where(sp => sp.Idth == idth).ToList();

            if (sanpham == null)
            {
                return NotFound();
            }

            return sanpham;
        }

        // PUT: api/Sanphams/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanpham(int id, Sanpham sanpham)
        {
            if (id != sanpham.Idsp)
            {
                return BadRequest();
            }

            _context.Entry(sanpham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanphamExists(id))
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

        // POST: api/Sanphams
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sanpham>> PostSanpham(Sanpham sanpham)
        {
            _context.Sanphams.Add(sanpham);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSanpham", new { id = sanpham.Idsp }, sanpham);
        }

        // DELETE: api/Sanphams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sanpham>> DeleteSanpham(int id)
        {
            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }

            _context.Sanphams.Remove(sanpham);
            await _context.SaveChangesAsync();

            return sanpham;
        }

        private bool SanphamExists(int id)
        {
            return _context.Sanphams.Any(e => e.Idsp == id);
        }
    }
}
