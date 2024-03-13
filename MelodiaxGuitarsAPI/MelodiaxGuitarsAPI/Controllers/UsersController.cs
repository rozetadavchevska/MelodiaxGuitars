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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var user = await _userRepository.GetAllAsync();
            var userDto = _mapper.Map<List<UserDto>>(user);
            return Ok(userDto);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
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

            if (!string.IsNullOrEmpty(userDto.PasswordHash))
            {
                userToUpdate.PasswordHash = PasswordHasher.HashPassword(userDto.PasswordHash);
            }

            await _userRepository.UpdateUserAsync(id, userToUpdate);
            return NoContent();
        }

        // POST: api/Users
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = PasswordHasher.HashPassword(userDto.PasswordHash);
            await _userRepository.AddUserAsync(user);

            var createdUser = _mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
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
        public async Task<ActionResult<User>> Login (User userRequest)
        {
            var user = await _userRepository.GetUserByEmail(userRequest.Email);
            if(user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            if (!PasswordHasher.VerifyPassword(userRequest.PasswordHash, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            var userEntity = _mapper.Map<UserDto>(user);
            string token = GenerateToken(userEntity);

            return Ok(token);
        }

        private string GenerateToken(UserDto userDto)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userDto.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
