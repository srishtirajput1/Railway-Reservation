using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace RailwayReservation.Repositories
{
    public class ClassRepository : IClass
    {
        private readonly OnlineRailwayReservationSystemDbContext _context;

        public ClassRepository(OnlineRailwayReservationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAll()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<Class> GetById(string id)
        {
            return await _context.Classes.FirstOrDefaultAsync(c => c.ClassId == id);
        }

        public async Task<IEnumerable<Class>> SearchByClassName(string className)
        {
            return await _context.Classes
                .Where(c => c.ClassName == className)
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetByClassType(string classType)
        {
            return await _context.Classes
                .Where(c => c.ClassType == classType)
                .ToListAsync();
        }

        public async Task<Class> AddClass(Class classEntity)
        {
            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();
            return classEntity;
        }

        public async Task UpdateClass(string id, Class classEntity)
        {
            var existingClass = await _context.Classes.FindAsync(id);
            if (existingClass != null)
            {
                existingClass.ClassName = classEntity.ClassName;
                existingClass.ClassType = classEntity.ClassType;
                existingClass.AvailableSeats = classEntity.AvailableSeats;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteClass(string id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity != null)
            {
                _context.Classes.Remove(classEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
