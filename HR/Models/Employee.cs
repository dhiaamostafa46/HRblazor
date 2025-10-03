using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string JobNumber { get; set; }

        [StringLength(15)]
        public string MobileNumber { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        // Initialize the collection
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    }
}
