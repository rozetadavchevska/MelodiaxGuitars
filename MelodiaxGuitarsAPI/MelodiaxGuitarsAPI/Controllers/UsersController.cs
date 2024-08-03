using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Users;
using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;
using MelodiaxGuitarsAPI.Utilities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using MelodiaxGuitarsAPI.Repositories.ShoppingCarts;
using Microsoft.AspNetCore.Authorization;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var user = await _userRepository.GetAllAsync();
            var userDto = _mapper.Map<List<UserDto>>(user);
            return Ok(userDto);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user); 
            return Ok(userDto);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(string id, UserDto userDto)
        {
            var userToUpdate = await _userRepository.GetUserById(id);
            if(userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FirstName = userDto.FirstName;
            userToUpdate.LastName = userDto.LastName;
            userToUpdate.Email = userDto.Email;
            userToUpdate.PhoneNumber = userDto.PhoneNumber;
            userToUpdate.Address = userDto.Address;
            userToUpdate.City = userDto.City;
            userToUpdate.Country = userDto.Country;

            await _userRepository.UpdateUserAsync(id, userToUpdate);
            return NoContent();
        }

        // POST: api/Users/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            var existingUser = await _userRepository.GetUserByEmail(userDto.Email);

            if (existingUser != null)
            {
                return BadRequest("User with this email already exists.");
            }

            string role = IsAdminEmail(userDto.Email) ? "Admin" : "User";

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = PasswordHasher.HashPassword(userDto.PasswordHash);

            await _userRepository.AddUserAsync(user, role);

            var createdUser = _mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login (string email, string passwordHash)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null || !PasswordHasher.VerifyPassword(passwordHash, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }
            var role = IsAdminEmail(user.Email) ? "Admin" : "User";
            var token = GenerateToken(user, role);
            return Ok(token);
        }

        private string GenerateToken(User user, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Token"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool IsAdminEmail(string email)
        {
            return email.EndsWith("@melodiax.com") && email.StartsWith("admin");
        }
    }
}
