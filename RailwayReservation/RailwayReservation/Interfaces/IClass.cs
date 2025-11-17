using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface IClass
    {
        Task<IEnumerable<Class>> GetAll();
        Task<Class> GetById(string id);
        Task<IEnumerable<Class>> SearchByClassName(string className);
        Task<IEnumerable<Class>> GetByClassType(string classType);
        Task<Class> AddClass(Class classEntity);
        Task UpdateClass(string id, Class classEntity);
        Task<bool> DeleteClass(string id);
    }
}
