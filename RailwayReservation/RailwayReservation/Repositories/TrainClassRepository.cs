using Microsoft.EntityFrameworkCore;
using System;
using RailwayReservation.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using RailwayReservation.Interfaces;

public class TrainClassRepository : ITrainClass
{
    private readonly OnlineRailwayReservationSystemDbContext _context;

    public TrainClassRepository(OnlineRailwayReservationSystemDbContext context)
    {
        _context = context;
    }

    // Create a new TrainClass
    public async Task<TrainClass> CreateTrainClassAsync(TrainClass trainClass)
    {
        if (trainClass == null)
        {
            throw new ArgumentNullException(nameof(trainClass));
        }

        _context.TrainClasses.Add(trainClass);
        await _context.SaveChangesAsync();
        return trainClass;
    }

    public async Task<TrainClass> GetTrainClassByIdAsync(int id)
    {
        return await _context.TrainClasses
            .Include(tc => tc.Class)
            .Include(tc => tc.Train)
            .FirstOrDefaultAsync(tc => tc.TrainClassId == id);
    }

    // Get all TrainClasses
    public async Task<List<TrainClass>> GetAllTrainClassesAsync()
    {
        return await _context.TrainClasses
            .Include(tc => tc.Class)
            .Include(tc => tc.Train)
            .ToListAsync();
    }
   
    // Update TrainClass
    public async Task<TrainClass?> UpdateTrainClassAsync(int id, TrainClass updatedTrainClass)
    {
        var trainClass = await _context.TrainClasses
            .FirstOrDefaultAsync(tc => tc.TrainClassId == id);

        if (trainClass == null)
        {
            return null; // or throw an exception
        }

        // Update fields
        trainClass.ClassId = updatedTrainClass.ClassId;
        trainClass.RouteId = updatedTrainClass.RouteId;
        trainClass.TrainId = updatedTrainClass.TrainId;

        await _context.SaveChangesAsync();
        return trainClass;
    }
 
    // Delete TrainClass by TrainClassId
    public async Task<bool> DeleteTrainClassAsync(int id)
    {
        var trainClass = await _context.TrainClasses.FindAsync(id);

        if (trainClass == null)
        {
            return false;
        }

        _context.TrainClasses.Remove(trainClass);
        await _context.SaveChangesAsync();
        return true;
    }
}
