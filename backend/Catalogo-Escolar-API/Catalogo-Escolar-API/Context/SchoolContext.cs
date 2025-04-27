using Catalogo_Escolar_API.Model;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Represents the class context of database
/// </summary>
public class SchoolContext : DbContext
{
    /// <summary>
    /// Constructor of context class
    /// </summary>
    /// <param name="options"></param>
    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Users table
    /// </summary>
    public DbSet<User> Users { get; set; }
    /// <summary>
    /// Students table
    /// </summary>
    public DbSet<Student> Students { get; set; }
    /// <summary>
    /// Teacher table
    /// </summary>
    public DbSet<Teacher> Teachers { get; set; }
    /// <summary>
    /// Classes table
    /// </summary>
    public DbSet<Class> Classes { get; set; }
    /// <summary>
    /// Student-Classes table
    /// </summary>
    public DbSet<StudentClass> StudentClasses { get; set; }
    /// <summary>
    /// Grades table
    /// </summary>
    public DbSet<Grade> Grades { get; set; }
    /// <summary>
    /// Assignments table
    /// </summary>
    public DbSet<Assignment> Assignments { get; set; }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentClass>()
            .HasKey(sc => new { sc.StudentId, sc.ClassId });

        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentClasses)
            .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Class)
            .WithMany(c => c.StudentClasses)
            .HasForeignKey(sc => sc.ClassId);

        // Grade ↔ Student
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId);

        // Grade ↔ Class
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Class)
            .WithMany(c => c.Grades)
            .HasForeignKey(g => g.ClassId);

        // Grade ↔ Assignment
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Assignment)
            .WithMany(a => a.Grades)
            .HasForeignKey(g => g.AssignmentId);

        // Student ↔ User (1:1)
        modelBuilder.Entity<Student>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId);

        // Teacher ↔ User (1:1)
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        // Class ↔ Teacher (1:M)
        modelBuilder.Entity<Class>()
            .HasOne(c => c.Teacher)
            .WithMany(t => t.Classes)
            .HasForeignKey(c => c.TeacherId);

        // Assignment ↔ Class
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Class)
            .WithMany(c => c.Assignments)
            .HasForeignKey(a => a.ClassId);
    }
}
