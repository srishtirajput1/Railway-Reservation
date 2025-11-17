using RailwayReservation.Models;
using RailwayReservation.ViewModels;

namespace RailwayReservation.Interfaces
{
    public interface IPassengerDetail
    {

        List<PassengerDetail> AddPassenger(List<PassengerDTO> passengers, string userId);
        Task<bool> UpdatePassengerDetailAsync(PassengerDetail passengerDetail);

        Task<IEnumerable<PassengerDetail>> GetAllPassengerDetailsAsync();
        Task<PassengerDetail> GetPassengerDetailByIdAsync(string passengerId);
        Task<bool> DeletePassengerDetailAsync(string passengerId);
    }
}
