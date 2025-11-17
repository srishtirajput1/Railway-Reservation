

using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface IQueryList
    {
        Task AddQueryList(QueryList queryList);
        Task<QueryList> GetQueryListById(string queryListId);
        Task<IEnumerable<QueryList>> GetQueryListsByQueryId(string queryId);
        Task UpdateQueryList(QueryList queryList);
        Task DeleteQueryList(string queryListId);
    }
}
