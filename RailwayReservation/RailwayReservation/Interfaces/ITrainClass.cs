using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface ITrainClass
    {
        public interface ITrainClassRepository
        {
            Task<TrainClass> CreateTrainClassAsync(TrainClass trainClass);
            Task<TrainClass?> GetTrainClassByIdAsync(int id);
            Task<IEnumerable<TrainClass>> GetAllTrainClassesAsync();
            Task<TrainClass> UpdateTrainClassAsync(int id, TrainClass updatedTrainClass);
            Task<bool> DeleteTrainClassAsync(int id);
        }

    }
}
