using HR.Data;
using HR.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace HR.Services
{
    public class LeaveTypeService
    {
        private readonly AppDbContext _context;

        public LeaveTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveType>> GetAllLeaveTypesAsync()
        {
            return await _context.LeaveTypes.ToListAsync();
        }

        public async Task<LeaveType> GetLeaveTypeByIdAsync(int id)
        {
            return await _context.LeaveTypes.FindAsync(id);
        }

        public async Task AddLeaveTypeAsync(LeaveType leaveType)
        {
            if (leaveType == null)
            {
                throw new ArgumentNullException(nameof(leaveType));
            }

            _context.LeaveTypes.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeaveTypeAsync(LeaveType leaveType)
        {
            if (leaveType == null)
            {
                throw new ArgumentNullException(nameof(leaveType));
            }

            _context.LeaveTypes.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeaveTypeAsync(int id)
        {
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                _context.LeaveTypes.Remove(leaveType);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Optionally handle the case when the leave type is not found
                throw new KeyNotFoundException($"LeaveType with ID {id} not found.");
            }
        }


        
    }
}
