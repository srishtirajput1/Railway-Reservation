using System.ComponentModel.DataAnnotations;
using RailwayReservation.Models;
using RailwayReservation.ViewModels;

namespace RailwayReservation.Interfaces
{
    public interface ITrain
    {
        bool TrainExists(string id);
        Task<Train> GetTrainByIdAsync(string trainId);
        Task<List<Train>> GetAllTrainsAsync();
        Task<bool> AddTrainSeatsAsync(string trainId, List<string> classIds);
        Train AddTrainDetails(TrainVM trainVm);

        bool CheckAvailability(string trainId, string quota, int passCount);

        bool UpdateTrain(string trainId, TrainVM updatedTrain);

        bool DeleteTrain(string trainId);
    }
}
