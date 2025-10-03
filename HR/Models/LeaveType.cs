using System.ComponentModel.DataAnnotations;

namespace HR.Models
{
    public class LeaveType
    {
        [Key]
        public int LeaveTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }
    }
}
