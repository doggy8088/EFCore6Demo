using System;
using System.Collections.Generic;

namespace EFCore6Demo.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
            Instructors = new HashSet<Person>();
        }

        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Person> Instructors { get; set; }
    }
}
