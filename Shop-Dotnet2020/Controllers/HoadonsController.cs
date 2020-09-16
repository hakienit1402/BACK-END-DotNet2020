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
    public class HoadonsController : ControllerBase
    {
        ShopDotnetContext _context = new ShopDotnetContext();


        // GET: api/Hoadons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hoadon>>> GetHoadons()
        {
            return await _context.Hoadons.Include(hd => hd.Chitiethoadons)
                .ToListAsync();
        }

       /* private object GetTaikhoan(Hoadon idkh)
        {
            throw new NotImplementedException();
        }*/

        // GET: api/Hoadons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hoadon>> GetHoadon(int id)
        {
            var hoadon = await _context.Hoadons
                .Include(hd => hd.Chitiethoadons)
                .Where(hd => hd.Idhd == id)
                .FirstOrDefaultAsync();


            if (hoadon == null)
            {
                return NotFound();
            }

            return hoadon;
        }

        
        

        // PUT: api/Hoadons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutHoadon(int id, Hoadon hoadon)
        {
            if (id != hoadon.Idhd)
            {
                return false;
            }

            _context.Entry(hoadon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoadonExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        // POST: api/Hoadons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hoadon>> PostHoadon(Hoadon hoadon)
        {
            _context.Hoadons.Add(hoadon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoadon", new { id = hoadon.Idhd }, hoadon);
        }

        // DELETE: api/Hoadons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hoadon>> DeleteHoadon(int id)
        {
            var hoadon = await _context.Hoadons
               .Include(hd => hd.Chitiethoadons)
               .Where(hd => hd.Idhd == id)
               .FirstOrDefaultAsync();
            if (hoadon == null)
            {
                return NotFound();
            }

            _context.Hoadons.Remove(hoadon);
            await _context.SaveChangesAsync();

            return hoadon;
        }

        private bool HoadonExists(int id)
        {
            return _context.Hoadons.Any(e => e.Idhd == id);
        }
    }
}
