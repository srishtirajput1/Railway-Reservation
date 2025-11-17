 using Microsoft.EntityFrameworkCore;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using FluentValidation;
using RailwayReservation.Validators;
using Microsoft.AspNetCore.Identity;
using RailwayReservation.ViewModels;

namespace RailwayReservation.Repositories
{
    public class UserRepository : IUsers
    {
        private readonly OnlineRailwayReservationSystemDbContext _context;
        private readonly UserValidator _userValidator;
        public UserRepository(OnlineRailwayReservationSystemDbContext context, UserValidator userValidator)
        {
            _context = context;
            _userValidator = userValidator;
        }

        public async Task<(bool, string)> AddUser(RegisterModel model, string salt, string hashedPassword)
        {
            User user = new User
            {
                UserId = model.UserId,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Role = model.Role,
                HashPassword = hashedPassword,
                SaltPassword = salt
            };

            var validationResult = _userValidator.Validate(user);
            if(validationResult.IsValid)
            {
                var userExists = _context.Users.Where(u => u.UserId == user.UserId || u.Email == user.Email);
                //Console.WriteLine("user searched");
                if(!userExists.Any())
                {
                    //Console.WriteLine("inside if");
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return (true, "User added successfully");
                }
                else
                {
                    //Console.WriteLine("inside else"); 
                    return (false, "User already exists");
                }
            }

            var errorMessages = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));

            return (false, errorMessages);
        }

        public async Task<User> GetUser(string userId, string email)
        {
            //if (userId != null)
            //{
                return await _context.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId || u.Email ==email);
            //}
            //else if (email != null)
            //{
            //    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            //}
            //return null;
        }

        // Get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Update a user
        public async Task<bool> UpdateUserAsync(UserVM userObj)
        {
            User user = new User()
            {
                UserId = userObj.UserId,
                Name = userObj.Name,
                Email = userObj.Email,
                Phone = userObj.Phone,

            };
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a user
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
