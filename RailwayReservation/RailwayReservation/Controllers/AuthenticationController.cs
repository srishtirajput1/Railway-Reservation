using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Services;
using RailwayReservation.ViewModels;
using RailwayReservation.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService _tService;
        private readonly IUsers _userRepo;
        private readonly PasswordHasher _passwordHasher;

        public AuthenticationController(TokenService tokenService, IUsers userRepository, PasswordHasher passwordHasher)
        {
            _tService = tokenService;
            _userRepo = userRepository;
            _passwordHasher = passwordHasher;
        }

        // POST: api/auth/register
        [HttpPost("resgister")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if(!_passwordHasher.IsPasswordValid(registerModel.Password))
            {
                return BadRequest(new { Message = "Invalid Password" });
            }

            var existingUser = await _userRepo.GetUser(registerModel.UserId, registerModel.Email);
            if(existingUser != null)
            {
                return BadRequest(new { Message = "Username or Email already exists" });
            }

            string salt;
            string hashedPassword = _passwordHasher.HashPassword(registerModel.Password, out salt);

            var isUserAdded = await _userRepo.AddUser(registerModel, salt, hashedPassword);

            if(isUserAdded.Item1)
            {
                return Ok(new { Message = "User registered successfully" });
            }
            //else
            //{
                return BadRequest(new { Message = "User registration failed", Error = isUserAdded.Item2 });
            //}
        }

        // POST: api/with/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var userObj = await _userRepo.GetUser(loginModel.UserId, loginModel.Email);
            if (userObj == null)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            // Verify the entered password
            bool isPasswordValid = _passwordHasher.VerifyPassword(loginModel.Password, userObj.HashPassword, userObj.SaltPassword);

            if (!isPasswordValid)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            //generate JWTToken on the basis of userId and userrole
            var token = _tService.GenerateToken(userObj.UserId, userObj.Role);
            return Ok(new { Message = "Login successful", user = userObj, Token = token });
        }

    }
}
