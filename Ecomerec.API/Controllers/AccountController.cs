using Ecomerec.BLL.DTOs.AccoundDtos;
using Ecomerec.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecomerec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> user)
        {
            _userManager = user;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDTO dto)
        {
            if (dto is null)
            {
                return BadRequest("Invalid data");
            }

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                Cart = new Cart()
            };
            var result = await _userManager.CreateAsync(user,dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    Errors = result.Errors.Select(x => new { x.Code, x.Description })
                });
            }
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO dto)
        {
            if (dto is null)
            {
                return BadRequest("Invalid data");
            }
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user is null)
                return NotFound("This user is not found");

            var password = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!password)
                return Unauthorized();

            string secertKey = "AhmedAlaaAhmedAlistrongpasswordandstrongsercretkey";
            var EncodedKey = Encoding.UTF8.GetBytes(secertKey);
            var key = new SymmetricSecurityKey(EncodedKey);
            var signCredintial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Email, user!.Email),
                new Claim(ClaimTypes.MobilePhone , user.PhoneNumber),
            };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5275",
                audience: "http://localhost:4000",
                signingCredentials: signCredintial,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(7)
                );

            var handler = new JwtSecurityTokenHandler();
            var JwtToken = handler.WriteToken(token);
            return Ok(JwtToken);
        }


    }
}
