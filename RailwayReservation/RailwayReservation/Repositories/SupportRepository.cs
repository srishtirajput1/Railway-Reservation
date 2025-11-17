using RailwayReservation.Interfaces;
using Microsoft.EntityFrameworkCore;
using RailwayReservation.Models;

public class SupportRepository : ISupport
{
    private readonly OnlineRailwayReservationSystemDbContext _context;

    public SupportRepository(OnlineRailwayReservationSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Support>> GetAllSupportsAsync()
    {
        return await _context.Supports.ToListAsync();
    }

    public async Task<Support> GetSupportByIdAsync(string supportId)
    {
        return await _context.Supports.FindAsync(supportId);
    }

    public async Task AddSupportAsync(Support support)
    {
        _context.Supports.Add(support);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSupportAsync(Support support)
    {
        _context.Supports.Update(support);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSupportAsync(string supportId)
    {
        var support = await _context.Supports.FindAsync(supportId);
        if (support != null)
        {
            _context.Supports.Remove(support);
            await _context.SaveChangesAsync();
        }
    }
}
