using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface IQuery
    {
        Task AddQuery(Query query);
        Task<Query> GetQueryById(string queryId);
        Task<IEnumerable<Query>> GetAllQueries();
        Task<IEnumerable<Query>> GetQueriesByKeyword(string keyword);
    }
}
