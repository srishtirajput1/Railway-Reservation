using System.Linq;
using Microsoft.EntityFrameworkCore;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.ViewModels;

public class PassengerDetailRepository : IPassengerDetail
{
    private readonly OnlineRailwayReservationSystemDbContext _context;
    private readonly ILogger<PassengerDetail> _log;

    public PassengerDetailRepository(OnlineRailwayReservationSystemDbContext context, ILogger<PassengerDetail> log)
    {
        _context = context;
        _log = log;
    }

    public List<PassengerDetail> AddPassenger(List<PassengerDTO> passengers, string userId)
    {
        List<PassengerDetail> passes = new List<PassengerDetail>();
        try
        {
            User user = _context.Users.Find(userId);
            if (user == null)
            {
                return null;
            }
            foreach (var pass in passengers)
            {
                PassengerDetail passenger = new PassengerDetail()
                {
                    UserId = userId,
                    User = user,
                    Gender = pass.Gender,
                    Name = pass.Name,
                    Age = pass.Age,
                    PhoneNumber = pass.PhoneNumber
                };
                _context.PassengerDetails.Add(passenger);
                _context.SaveChanges();
                passes.Add(passenger);
            }
        }
        catch (Exception error)
        {
            _log.LogError(error.Message);
            return new List<PassengerDetail>();
        }

        return passes;

    }

    public async Task<PassengerDetail> GetPassengerDetailByIdAsync(string passengerId)
    {
        return _context.PassengerDetails.FirstOrDefault(p => p.PassengerId == passengerId);
    }

    public async Task<IEnumerable<PassengerDetail>> GetAllPassengerDetailsAsync()
    {
        return _context.PassengerDetails.ToList();
    }

    public async Task<bool> UpdatePassengerDetailAsync(PassengerDetail passengerDetail)
    {
        var existingPassenger = await GetPassengerDetailByIdAsync(passengerDetail.PassengerId);
        if (existingPassenger == null)
        {
            return false;
        }

        existingPassenger.Name = passengerDetail.Name;
        existingPassenger.Age = passengerDetail.Age;
        existingPassenger.Gender = passengerDetail.Gender;
        existingPassenger.CoachNumber = passengerDetail.CoachNumber;
        existingPassenger.SeatNumber = passengerDetail.SeatNumber;
        existingPassenger.BookingStatus = passengerDetail.BookingStatus;
        existingPassenger.TicketId = passengerDetail.TicketId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePassengerDetailAsync(string passengerId)
    {
        var passengerDetail = await GetPassengerDetailByIdAsync(passengerId);
        if (passengerDetail == null)
        {
            return false;
        }

        _context.PassengerDetails.Remove(passengerDetail);
        await _context.SaveChangesAsync();
        return true;
    }
}
