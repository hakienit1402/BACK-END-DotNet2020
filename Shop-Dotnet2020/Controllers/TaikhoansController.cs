using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop_Dotnet2020.Models;

namespace Shop_Dotnet2020.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class TaikhoansController : ControllerBase
    {
        ShopDotnetContext _context = new ShopDotnetContext();
        private IConfiguration _config;

        public TaikhoansController(IConfiguration config)
        {
            _config = config;
        }


        [HttpPost("quenpass")]
        public ActionResult<Taikhoan> ForgotPass(Taikhoan taikhoan)
        {

            Random ran = new Random();
            int a = ran.Next(100000, 999999);

            var user = _context.Taikhoans
                                     .Where(s => s.Email == taikhoan.Email)
                                     .FirstOrDefault();
            if (user != null)
            {
                new Mail().sendMail(taikhoan.Email, "Mã xác nhận", "Mã xác nhận của bạn là: " + a);

                return Ok(new { a, user });
            }
            return Ok();

        }
        [HttpPost("forgetpass")]
        public async Task<ActionResult<Taikhoan>> ForgetPass(Forget forget)
        {

            var user = _context.Taikhoans
                                     .Where(s => s.Email == forget.Email)
                                     .FirstOrDefault();
            if (user != null)
            {
                user.Pass = forget.Pass;
                await _context.SaveChangesAsync();

            }
            return Ok(new { user });

        }
        //PUT: api/taikhoans/resetpass
        [HttpPut("doipass/{id}")]
        public async Task<ActionResult<bool>> ResetPass(int id, Taikhoan tk)
        {
            if (id != tk.Idkh)
            {
                return false;
            }

            _context.Entry(tk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaikhoanExists(id))
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


        [HttpPost("login")]
        public IActionResult Login(Taikhoan taikhoan)
        {
            var user = _context.Taikhoans
                                     .Where(s => s.Username == taikhoan.Username 
                                     && s.Pass == taikhoan.Pass 
                                     )
                                     .FirstOrDefault();
            IActionResult response = Unauthorized();
            if (user != null && user.Status == "1")
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr,user
                });

                return response;
            }
            else
            {
                return Ok();
            }

        }
        private string GenerateJSONWebToken(Taikhoan userinfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                 new Claim("Idkh", userinfo.Idkh.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,userinfo.Username),

                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
        // GET: api/Taikhoan/user
        /* [HttpGet("user")]
         [Authorize]
         public async Task<ActionResult<Object>> GetUserProfile()
         {
             var identity = HttpContext.User.Identity as ClaimsIdentity;
             IList<Claim> claim = identity.Claims.ToList();
             var id = claim[0].Value;
             int userId = int.Parse(id);
             var taikhoan = await _context.Taikhoan.FindAsync(userId);

             return taikhoan;
         }*/
        // POST: api/Taikhoan/register
        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(Taikhoan taikhoan)
        {
            var user = _context.Taikhoans
                                     .Where(s => s.Username == taikhoan.Username)
                                     .FirstOrDefault();
            if (user != null)
            {
                return false;

            }
            else
   
                 _context.Taikhoans.Add(taikhoan);
       
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaikhoan", new { id = taikhoan.Idkh }, taikhoan);
        }
        // POST: api/Taikhoan/CheckUsernameIsExits
        [HttpPost("CheckUsernameIsExits")]
        public ActionResult<bool> CheckUsernameIsExits(string username)
        {
            var user = _context.Taikhoans
                                     .Where(s => s.Username == username)
                                     .FirstOrDefault();
            if (user != null)
            {
                return true;

            }
            return false;
        }

        // GET: api/Taikhoans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Taikhoan>>> GetTaikhoans()
        {
            return await _context.Taikhoans .Include(hd => hd.Hoadons).ToListAsync();
        }

        // GET: api/Taikhoans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Taikhoan>> GetTaikhoan(int id)
        {
           var taikhoan = await _context.Taikhoans.Include(tk=>tk.Feedbacks).Include(tk => tk.Hoadons)
                .Where(tk => tk.Idkh == id)
                .FirstOrDefaultAsync();

            if (taikhoan == null)
            {
                return NotFound();
            }

            return taikhoan;
        }

        // PUT: api/Taikhoans/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaikhoan(int id, Taikhoan taikhoan)
        {
            if (id != taikhoan.Idkh)
            {
                return BadRequest();
            }

            _context.Entry(taikhoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaikhoanExists(id))
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

        // POST: api/Taikhoans
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Taikhoan>> PostTaikhoan(Taikhoan taikhoan)
        {
           
            _context.Taikhoans.Add(taikhoan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaikhoan", new { id = taikhoan.Idkh }, taikhoan);
        }

        // DELETE: api/Taikhoans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Taikhoan>> DeleteTaikhoan(int id)
        {
            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            _context.Taikhoans.Remove(taikhoan);
            await _context.SaveChangesAsync();

            return taikhoan;
        }

        private bool TaikhoanExists(int id)
        {
            return _context.Taikhoans.Any(e => e.Idkh == id);
        }
    }
}
