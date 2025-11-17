using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RailwayReservation.Models;

public partial class OnlineRailwayReservationSystemDbContext : DbContext
{
    public OnlineRailwayReservationSystemDbContext()
    {
    }

    public OnlineRailwayReservationSystemDbContext(DbContextOptions<OnlineRailwayReservationSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassCoach> ClassCoaches { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Fare> Fares { get; set; }

    public virtual DbSet<PassengerDetail> PassengerDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Query> Queries { get; set; }

    public virtual DbSet<QueryList> QueryLists { get; set; }

    public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Support> Supports { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<TrainClass> TrainClasses { get; set; }

    public virtual DbSet<TrainRoute> TrainRoutes { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(localdb)\\MsSqlLocalDb;Integrated Security=true;Trusted_Connection=True;Database=OnlineRailwayReservationSystemDb;TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927C09461449A");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ClassName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ClassType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClassCoach>(entity =>
        {
            entity.HasKey(e => e.ClassCoachId).HasName("PK__ClassCoa__FC6359FDD3BFC34A");

            entity.ToTable("ClassCoach");

            entity.Property(e => e.CoachId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Coach).WithMany(p => p.ClassCoaches)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK__ClassCoac__Coach__32AB8735");

            entity.HasOne(d => d.TrainClass).WithMany(p => p.ClassCoaches)
                .HasForeignKey(d => d.TrainClassId)
                .HasConstraintName("FK__ClassCoac__Train__31B762FC");
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.CoachId).HasName("PK__Coach__F411D9411E387FDD");

            entity.ToTable("Coach");

            entity.Property(e => e.CoachId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ClassId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CoachNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Fare>(entity =>
        {
            entity.HasKey(e => e.FareId).HasName("PK__Fare__1261FA163B2AF02B");

            entity.ToTable("Fare");

            entity.Property(e => e.FareId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CancelCharge12hrs).HasColumnName("CancelCharge_12hrs");
            entity.Property(e => e.CancelCharge48hrs).HasColumnName("CancelCharge_48hrs");
            entity.Property(e => e.CancelCharge4hrs).HasColumnName("CancelCharge_4hrs");
            entity.Property(e => e.ClassId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Fares)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Fare__ClassId__47DBAE45");
        });

        modelBuilder.Entity<PassengerDetail>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915FB029CBD27A");

            entity.ToTable("PassengerDetail");

            entity.Property(e => e.PassengerId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CoachNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TicketId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Ticket).WithMany(p => p.PassengerDetails)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Passenger__Ticke__06CD04F7");

            entity.HasOne(d => d.User).WithMany(p => p.PassengerDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Passenger__UserI__3B40CD36");
        });

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

        modelBuilder.Entity<Query>(entity =>
        {
            entity.HasKey(e => e.QueryId).HasName("PK__Query__5967F7DB55D6C226");

            entity.ToTable("Query");

            entity.Property(e => e.QueryId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Keywords)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<QueryList>(entity =>
        {
            entity.HasKey(e => e.QueryListId).HasName("PK__QueryLis__ACD72B5FFD3A8083");

            entity.ToTable("QueryList");

            entity.Property(e => e.QueryListId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.QueryDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.QueryId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Query).WithMany(p => p.QueryLists)
                .HasForeignKey(d => d.QueryId)
                .HasConstraintName("FK__QueryList__Query__5812160E");
        });

        modelBuilder.Entity<ReservationDetail>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F245B61F91F");

            entity.Property(e => e.ReservationId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PassengerId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PaymentId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.QuotaName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TicketId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TotalFare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrainId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Passenger).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__Reservati__Passe__45BE5BA9");

            entity.HasOne(d => d.Payment).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Payme__534D60F1");

            entity.HasOne(d => d.Ticket).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Ticke__5070F446");

            entity.HasOne(d => d.Train).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Reservati__Train__44CA3770");

            entity.HasOne(d => d.User).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__UserI__5165187F");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__Seat__311713F35F7E69D4");

            entity.ToTable("Seat");

            entity.Property(e => e.Quota)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.ClassCoach).WithMany(p => p.Seats)
                .HasForeignKey(d => d.ClassCoachId)
                .HasConstraintName("FK__Seat__ClassCoach__3587F3E0");
        });

        modelBuilder.Entity<Support>(entity =>
        {
            entity.HasKey(e => e.SupportId).HasName("PK__Support__D82DBC8C65F15FA7");

            entity.ToTable("Support");

            entity.Property(e => e.SupportId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.QueryListId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.QueryText)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.QueryList).WithMany(p => p.Supports)
                .HasForeignKey(d => d.QueryListId)
                .HasConstraintName("FK__Support__QueryLi__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.Supports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Support__UserId__5AEE82B9");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC6074E53AE41");

            entity.ToTable("Ticket");

            entity.HasIndex(e => e.Pnr, "UQ_Ticket_PNR").IsUnique();

            entity.Property(e => e.TicketId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
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
            entity.Property(e => e.QuotaName)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.Property(e => e.TrainName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TrainNumber)
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
            entity.Property(e => e.DestinationStation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JourneyEndDate).HasColumnType("datetime");
            entity.Property(e => e.JourneyStartDate).HasColumnType("datetime");
            entity.Property(e => e.RouteId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("RouteID");
            entity.Property(e => e.RunningDay)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SeatFare).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SourceStation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TrainName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TrainNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.TrainRoute).WithMany(p => p.Trains)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__Train__RouteID__3A4CA8FD");
        });

        modelBuilder.Entity<TrainClass>(entity =>
        {
            entity.HasKey(e => e.TrainClassId).HasName("PK__TrainCla__7FBBCD94F1FEB43A");

            entity.ToTable("TrainClass");

            entity.Property(e => e.ClassId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RouteId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TrainId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.TrainClasses)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__TrainClas__Class__2DE6D218");

            entity.HasOne(d => d.Route).WithMany(p => p.TrainClasses)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__TrainClas__Route__2EDAF651");

            entity.HasOne(d => d.Train).WithMany(p => p.TrainClasses)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__TrainClas__Train__2CF2ADDF");
        });

        modelBuilder.Entity<TrainRoute>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Route__80979AAD9EFF58F3");

            entity.ToTable("TrainRoute");

            entity.Property(e => e.RouteId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("RouteID");
            entity.Property(e => e.Destination)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Source)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B6CCDC1A4");

            entity.Property(e => e.CardHolderName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Cvv).HasColumnName("CVV");
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
