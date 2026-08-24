using System;
using System.Collections.Generic;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public partial class VirtualBagDbContext : DbContext
{
    public VirtualBagDbContext()
    {
    }

    public VirtualBagDbContext(DbContextOptions<VirtualBagDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<AttendanceSession> AttendanceSessions { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Homework> Homeworks { get; set; }

    public virtual DbSet<HomeworkSubmission> HomeworkSubmissions { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<StudyActivity> StudyActivities { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<TeacherAssignment> TeacherAssignments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69261CF0C98C49");

            entity.ToTable("Attendance");

            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Session).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__Sessi__52593CB8");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__Stude__534D60F1");
        });

        modelBuilder.Entity<AttendanceSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Attendan__C9F4929018334608");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Class).WithMany(p => p.AttendanceSessions)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__Class__4E88ABD4");

            entity.HasOne(d => d.Subject).WithMany(p => p.AttendanceSessions)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__Subje__4F7CD00D");

            entity.HasOne(d => d.Teacher).WithMany(p => p.AttendanceSessions)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__Teach__4D94879B");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207DEDCB2F7");

            entity.Property(e => e.Author)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FilePath)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Class).WithMany(p => p.Books)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__ClassId__37A5467C");

            entity.HasOne(d => d.Subject).WithMany(p => p.Books)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__SubjectId__38996AB5");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__UploadedB__398D8EEE");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927C0E42A83C4");

            entity.Property(e => e.ClassName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Section)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Homework>(entity =>
        {
            entity.HasKey(e => e.HomeworkId).HasName("PK__Homework__FDE46A72F5EE6539");

            entity.ToTable("Homework");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Homework__ClassI__4316F928");

            entity.HasOne(d => d.Subject).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Homework__Subjec__440B1D61");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Homework__Teache__4222D4EF");
        });

        modelBuilder.Entity<HomeworkSubmission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__Homework__449EE125F0E29995");

            entity.Property(e => e.FilePath)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Submitted");
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Homework).WithMany(p => p.HomeworkSubmissions)
                .HasForeignKey(d => d.HomeworkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HomeworkS__Homew__48CFD27E");

            entity.HasOne(d => d.Student).WithMany(p => p.HomeworkSubmissions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HomeworkS__Stude__49C3F6B7");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Notes__EACE355F386CAB6A");

            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoteColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Notes)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notes__StudentId__3D5E1FD2");

            entity.HasOne(d => d.Subject).WithMany(p => p.Notes)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notes__SubjectId__3E52440B");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12E274908D");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(250);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__5EBF139D");
        });

        modelBuilder.Entity<StudyActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__StudyAct__45F4A791D8C3D7A4");

            entity.Property(e => e.ActivityDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.StudyActivities)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudyActi__Stude__571DF1D5");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3A862679AF0");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subjects__ClassI__286302EC");
        });

        modelBuilder.Entity<TeacherAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__TeacherA__32499E77D37E2927");

            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Class).WithMany(p => p.TeacherAssignments)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeacherAs__Class__32E0915F");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeacherAssignments)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeacherAs__Subje__33D4B598");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherAssignments)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeacherAs__Teach__31EC6D26");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C618916C5");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053405A15E5B").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Class).WithMany(p => p.Users)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Users__ClassId__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
