using RailwayReservation.ViewModels;
using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface IUsers
    {
        //Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUser(string userId, string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(UserVM userObj);
        Task<bool> DeleteUserAsync(string userId);
        Task<(bool, string)> AddUser(RegisterModel model, string salt, string hashedPassword);

    }
}
