using System;
using System.Collections.Generic;

namespace EFCore6Demo.Models
{
    public partial class Person
    {
        public Person()
        {
            Departments = new HashSet<Department>();
            Enrollments = new HashSet<Enrollment>();
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public DateTime? HireDate { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string Discriminator { get; set; } = null!;

        public virtual OfficeAssignment OfficeAssignment { get; set; } = null!;
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
