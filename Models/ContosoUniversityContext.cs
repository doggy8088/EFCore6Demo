using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCore6Demo.Models
{
    public partial class ContosoUniversityContext : DbContext
    {
        public ContosoUniversityContext()
        {
        }

        public ContosoUniversityContext(DbContextOptions<ContosoUniversityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<VwCourseStudent> VwCourseStudents { get; set; } = null!;
        public virtual DbSet<VwCourseStudentCount> VwCourseStudentCounts { get; set; } = null!;
        public virtual DbSet<VwDepartmentCourseCount> VwDepartmentCourseCounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasIndex(e => e.DepartmentId, "IX_DepartmentID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_dbo.Course_dbo.Department_DepartmentID");

                entity.HasMany(d => d.Instructors)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseInstructor",
                        l => l.HasOne<Person>().WithMany().HasForeignKey("InstructorId").HasConstraintName("FK_dbo.CourseInstructor_dbo.Instructor_InstructorID"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").HasConstraintName("FK_dbo.CourseInstructor_dbo.Course_CourseID"),
                        j =>
                        {
                            j.HasKey("CourseId", "InstructorId").HasName("PK_dbo.CourseInstructor");

                            j.ToTable("CourseInstructor");

                            j.HasIndex(new[] { "CourseId" }, "IX_CourseID");

                            j.HasIndex(new[] { "InstructorId" }, "IX_InstructorID");

                            j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");

                            j.IndexerProperty<int>("InstructorId").HasColumnName("InstructorID");
                        });
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.HasIndex(e => e.InstructorId, "IX_InstructorID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Budget).HasColumnType("money");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_dbo.Department_dbo.Instructor_InstructorID");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollment");

                entity.HasIndex(e => e.CourseId, "IX_CourseID");

                entity.HasIndex(e => e.StudentId, "IX_StudentID");

                entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_dbo.Enrollment_dbo.Course_CourseID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_dbo.Enrollment_dbo.Person_StudentID");
            });

            modelBuilder.Entity<OfficeAssignment>(entity =>
            {
                entity.HasKey(e => e.InstructorId)
                    .HasName("PK_dbo.OfficeAssignment");

                entity.ToTable("OfficeAssignment");

                entity.HasIndex(e => e.InstructorId, "IX_InstructorID");

                entity.Property(e => e.InstructorId)
                    .ValueGeneratedNever()
                    .HasColumnName("InstructorID");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.HasOne(d => d.Instructor)
                    .WithOne(p => p.OfficeAssignment)
                    .HasForeignKey<OfficeAssignment>(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OfficeAssignment_dbo.Instructor_InstructorID");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Discriminator)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("('Instructor')");

                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<VwCourseStudent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwCourseStudents");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CourseTitle).HasMaxLength(50);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.StudentName).HasMaxLength(101);
            });

            modelBuilder.Entity<VwCourseStudentCount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwCourseStudentCount");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<VwDepartmentCourseCount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwDepartmentCourseCount");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
