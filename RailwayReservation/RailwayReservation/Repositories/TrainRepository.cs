using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using RailwayReservation.Validators;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.ViewModels;

namespace RailwayReservation.Repositories
{
    public class TrainRepository : ITrain
    {

        private readonly OnlineRailwayReservationSystemDbContext _context;

        private readonly TrainValidator _validator;
        private readonly ILogger<Train> _log;

        public TrainRepository(OnlineRailwayReservationSystemDbContext context, TrainValidator validator, ILogger<Train> log)
        {
            _context = context;
            _validator = validator;
            _log = log;
        }

        public bool TrainExists(string id)
        {
            return _context.Trains.Any(e => e.TrainId == id);
        }

        public async Task<Train> GetTrainByIdAsync(string trainId)
        {
            ValidateTrainId(trainId);
            var train = await _context.Trains.FindAsync(trainId);
            if (train == null)
                throw new KeyNotFoundException($"Train with ID '{trainId}' not found.");
            return train;

        }
        private void ValidateTrainId(string trainId)

        {

            if (string.IsNullOrWhiteSpace(trainId))

                throw new ArgumentException("Train ID cannot be null or empty.");

        }

        public async Task<List<Train>> GetAllTrainsAsync()
        {
            var res = _context.Trains.ToList();
            if(!res.Any())
            {
                return null;
            }

            return res;
        }

        public async Task AddTrainAsync(Train train)
        {
            await _context.Trains.AddAsync(train);
            await _context.SaveChangesAsync();
        }

        public Train AddTrainDetails(TrainVM trainVm)
        {
            try
            {
                var train = new Train()
                {
                    TrainId = trainVm.TrainId,
                    TrainNumber = trainVm.TrainNumber,
                    TrainName = trainVm.TrainName,
                    TotalSeats = trainVm.TotalSeats,
                    SourceStation = trainVm.SourceStation,
                    DestinationStation = trainVm.DestinationStation,
                    JourneyStartDate = trainVm.JourneyStartDate,
                    JourneyEndDate = trainVm.JourneyEndDate,
                    AvailableGeneralSeats = trainVm.AvailableGeneralSeat,
                    AvailableLadiesSeats = trainVm.AvailableLadiesSeat,
                    RunningDay = trainVm.RunningDay,
                    RouteId = trainVm.RouteId,
                    SeatFare = trainVm.SeatFare
                };
                _context.Trains.Add(train);
                _context.SaveChanges();
                return train;
            }
            catch (Exception error)
            {
                _log.LogError(error.Message);
            }
            return null;
        }

        public async Task<bool> AddTrainSeatsAsync(string trainId, List<string> classIds)
        {
            bool areSeatsAdded = false;

            foreach (var classId in classIds)
            {
                // Generate TrainClass
                var newTrainClass = new TrainClass { TrainId = trainId, ClassId = classId };
                _context.TrainClasses.Add(newTrainClass);
                await _context.SaveChangesAsync();

                // Generate ClassCoach and Seats
                string[] coachIds = GetCoachIds(classId);
                foreach (var coachId in coachIds)
                {
                    var classCoach = new ClassCoach { TrainClassId = newTrainClass.TrainClassId, CoachId = coachId };
                    _context.ClassCoaches.Add(classCoach);
                    await _context.SaveChangesAsync();

                    // Generate Seats
                    for (int i = 1; i <= 64; i++)
                    {
                        var seat = new Seat
                        {
                            SeatNumber = i,
                            ClassCoachId = classCoach.ClassCoachId,
                            Quota = i <= 6 ? "Ladies" : "General",
                            AvailabilityStatus = true
                        };
                        _context.Seats.Add(seat);
                        _context.SaveChanges();
                    }

                    await _context.SaveChangesAsync();
                }
            }

            areSeatsAdded = true;
            return areSeatsAdded;
        }

        private string[] GetCoachIds(string classId)
        {
            switch (classId)
            {
                case "CL101": return new[] { "CH01", "CH02" };
                case "CL102": return new[] { "CH03", "CH04" };
                case "CL103": return new[] { "CH05", "CH06" };
                case "CL104": return new[] { "CH07", "CH08" };
                case "CL105": return new[] { "CH09", "CH10" };
                case "CL106": return new[] { "CH11", "CH12" };
                default: return new string[] { };
            }
        }

        public Train GetTrain(string id)
        {
            try
            {
                return _context.Trains.FirstOrDefault(tr => tr.TrainId == id && tr.JourneyStartDate >= DateTime.Now);
            }
            catch
            {
                return null;
            }
        }
        public bool CheckAvailability(string trainId, string quota, int passCount)
        {
            Train train = GetTrain(trainId);
            if (train == null)
            {
                return false;
            }
            if (quota == "General")
            {
                if (train.AvailableGeneralSeats >= passCount)
                {
                    return true;
                }

            }
            else if (quota == "Ladies")
            {
                if (train.AvailableLadiesSeats >= passCount)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateTrain(string trainId, TrainVM updatedTrain)
        {
            var train = _context.Trains.Find(trainId);
            if (train == null) return false;

            try
            {
                train.TrainName = updatedTrain.TrainName;
                train.SourceStation = updatedTrain.SourceStation;
                train.DestinationStation = updatedTrain.DestinationStation;
                train.JourneyStartDate = updatedTrain.JourneyStartDate;
                train.JourneyEndDate = updatedTrain.JourneyEndDate;
                train.TotalSeats = updatedTrain.TotalSeats;
                train.AvailableGeneralSeats = updatedTrain.AvailableGeneralSeat;
                train.AvailableLadiesSeats = updatedTrain.AvailableLadiesSeat;
                train.SeatFare = updatedTrain.SeatFare;

                _context.Entry(train).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteTrain(string trainId)
        {
            var train = _context.Trains.Find(trainId);
            if (train == null) return false;

            try
            {
                _context.Trains.Remove(train);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return false;
            }
        }

    }
}
