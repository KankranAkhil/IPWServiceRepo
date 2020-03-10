using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IPWService.Models
{
    public partial class IPW_DevContext : DbContext
    {
        public IPW_DevContext()
        {
        }

        public IPW_DevContext(DbContextOptions<IPW_DevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Functionalities> Functionalities { get; set; }
        public virtual DbSet<RoleFunctionMapping> RoleFunctionMapping { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-28QL4E0;Database=IPW_Dev;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Functionalities>(entity =>
            {
                entity.HasKey(e => e.FunctionalityId);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Functionalities)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Functiona__Statu__36B12243");
            });

            modelBuilder.Entity<RoleFunctionMapping>(entity =>
            {
                entity.HasOne(d => d.Function)
                    .WithMany(p => p.RoleFunctionMapping)
                    .HasForeignKey(d => d.FunctionId)
                    .HasConstraintName("FK__RoleFunct__Funct__34C8D9D1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleFunctionMapping)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__RoleFunct__RoleI__33D4B598");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.RoleFunctionMapping)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__RoleFunct__Statu__35BCFE0A");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Roles__StatusId__37A5467C");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.EmailId).HasMaxLength(50);

                entity.Property(e => e.Fname)
                    .HasColumnName("FName")
                    .HasMaxLength(150);

                entity.Property(e => e.ImgPath).HasMaxLength(150);

                entity.Property(e => e.Lname)
                    .HasColumnName("LName")
                    .HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Users__CompanyId__3B75D760");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__3D5E1FD2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Users__StatusId__3C69FB99");
            });
        }
    }
}
