using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface ISupport
    {
        Task<IEnumerable<Support>> GetAllSupportsAsync();
        Task<Support> GetSupportByIdAsync(string supportId);
        Task AddSupportAsync(Support support);
        Task UpdateSupportAsync(Support support);
        Task DeleteSupportAsync(string supportId);


    }
}
