using System;
using System.Collections.Generic;

namespace EFCore6Demo.Models
{
    public partial class Department
    {
        public Department()
        {
            Courses = new HashSet<Course>();
        }

        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? InstructorId { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public virtual Person? Instructor { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
