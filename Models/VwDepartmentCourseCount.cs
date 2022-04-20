using System;
using System.Collections.Generic;

namespace EFCore6Demo.Models
{
    public partial class VwDepartmentCourseCount
    {
        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public int? CourseCount { get; set; }
    }
}
