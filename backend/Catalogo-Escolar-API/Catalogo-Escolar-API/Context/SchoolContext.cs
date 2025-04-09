using Microsoft.EntityFrameworkCore;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }
    public DbSet<Grade> Grades { get; set; }

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
    }
}
