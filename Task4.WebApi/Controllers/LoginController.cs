using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task4.WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Task4.Application.CQRS.RegisteredUsers.Commands.CreateRegisteredUser;
using Task4.Application.CQRS.RegisteredUsers.Commands.LoginUser;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Task4.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private readonly IMapper _mapper;

        public LoginController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] CreateRegisteredUserDto createUserDto)
        {
            var command = _mapper.Map<CreateRegisteredUserCommand>(createUserDto);
            var userId = await Mediator.Send(command);
            return Ok(GenerateJwt(CreateClaimList(createUserDto.Email!, userId), 
                Startup.Configuration!));
        }

        [HttpPut]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserDto loginUserDto)
        {
            var command = _mapper.Map<LoginUserCommand>(loginUserDto);
            var userId = await Mediator.Send(command);
            return Ok(GenerateJwt(CreateClaimList(loginUserDto.Email!, userId),
                Startup.Configuration!));
        }

        public static IEnumerable<Claim> CreateClaimList(string email, Guid id)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
        }

        public static string GenerateJwt(IEnumerable<Claim> claims, IConfiguration configuration)
        {
            var jwt = new JwtSecurityToken(
                issuer: configuration["Issuer"],
                audience: configuration["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(15)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]!)), 
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
