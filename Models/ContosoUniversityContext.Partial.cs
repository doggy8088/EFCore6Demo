using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCore6Demo.Models
{
    public partial class ContosoUniversityContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 1, Name = "教育訓練部", Budget = 1000, StartDate = DateTime.Now });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 2, Name = "人事行政部", Budget = 1000, StartDate = DateTime.Now });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 5, Name = "專案開發部", Budget = 1000, StartDate = DateTime.Now });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 13, Name = "產品開發部", Budget = 1000, StartDate = DateTime.Now });

            modelBuilder.Entity<Course>().HasData(new Course { CourseId = 1, Title = "Entity Framework 6 開發實戰", Credits = 1, DepartmentId = 5 });
            modelBuilder.Entity<Course>().HasData(new Course { CourseId = 2, Title = "Git新手入門", Credits = 1, DepartmentId = 5 });
            modelBuilder.Entity<Course>().HasData(new Course { CourseId = 3, Title = "Git進階版控流程", Credits = 2, DepartmentId = 5 });
            modelBuilder.Entity<Course>().HasData(new Course { CourseId = 4, Title = "ASP.NET MVC 5 開發實戰", Credits = 5, DepartmentId = 1 });
        }
    }
}
