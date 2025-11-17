using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PaymentMicroService.Models;

public partial class OnlineRailwayReservationSystemDbContext : DbContext
{
    public OnlineRailwayReservationSystemDbContext()
    {
    }

    public OnlineRailwayReservationSystemDbContext(DbContextOptions<OnlineRailwayReservationSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(localdb)\\MsSqlLocalDb;Integrated Security=true;Trusted_Connection=True;Database=OnlineRailwayReservationSystemDb;TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A3841ECCF0A");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TicketId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Ticket).WithMany(p => p.Payments)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__TicketI__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__UserId__4AB81AF0");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC6074E53AE41");

            entity.ToTable("Ticket");

            entity.HasIndex(e => e.Pnr, "UQ_Ticket_PNR").IsUnique();

            entity.Property(e => e.TicketId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ClassName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Coach)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.DestinationStation)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.JourneyEndDate).HasColumnType("datetime");
            entity.Property(e => e.JourneyStartDate).HasColumnType("datetime");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.SourceStation)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TicketStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TrainId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Train).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Ticket__TrainId__440B1D61");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Ticket__UserId__4316F928");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.TrainId).HasName("PK__Train__8ED2723A4AA63CB9");

            entity.ToTable("Train");

            entity.Property(e => e.TrainId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RouteId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RunningDay)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TrainName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TrainNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C9849D219");

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HashPassword)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SaltPassword)
                .HasMaxLength(24)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
