using HR.Data;
using HR.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Services
{
    public class LeaveRequestService
    {
        private readonly AppDbContext _context;

        public LeaveRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveRequest>> GetAllLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                .AsNoTracking()
                .Include(lr => lr.Employee)
                .Include(lr => lr.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .AsNoTracking()
                .Include(lr => lr.Employee)
                .Include(lr => lr.LeaveType)
                .FirstOrDefaultAsync(lr => lr.LeaveRequestId == id); // Use LeaveRequestId here

            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"LeaveRequest with ID {id} not found.");
            }

            return leaveRequest;
        }

        public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            if (leaveRequest == null)
            {
                throw new ArgumentNullException(nameof(leaveRequest));
            }

            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            if (leaveRequest == null)
            {
                throw new ArgumentNullException(nameof(leaveRequest));
            }

            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeaveRequestAsync(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                throw new KeyNotFoundException($"LeaveRequest with ID {id} not found.");
            }

            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();
        }
    }
}
