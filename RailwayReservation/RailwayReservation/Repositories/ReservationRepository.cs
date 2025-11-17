using Microsoft.EntityFrameworkCore;
using RailwayReservation.Models;
using RailwayReservation.Interfaces;
using RailwayReservation.ViewModels;

namespace RailwayReservation.Repositories
{
    public class ReservationRepository : IReservationDetail
    {

        private readonly OnlineRailwayReservationSystemDbContext _context;
        private readonly ILogger<ReservationDetail> _log;
        private readonly TrainRepository _trainRepo;

        public ReservationRepository(OnlineRailwayReservationSystemDbContext context, ILogger<ReservationDetail> log, TrainRepository trainRepo)
        {
            _context = context;
            _log = log;
            _trainRepo = trainRepo;
        }

    
        public Ticket AddReservation(ReservationDTO reservation, string userId, List<PassengerDetail> passengers)
        {
            List<ReservationDetail> reservations = new List<ReservationDetail>();
            List<PassengerTicket> passengerTickets = new List<PassengerTicket>();

            if (reservation != null && passengers.Count > 0)
            {
                try
                {
                    User user = _context.Users.Find(userId);
                    Train train = _context.Trains.Find(reservation.TrainId);
                    Random random = new Random();


                    //If Train Object is null
                    //Or
                    //Train Date is already gone then return NULL
                    if (train == null || DateTime.Now >= train.JourneyStartDate) return null;

                    int totalFare = (int)(passengers.Count * train.SeatFare);


                    //Getting the maximum transaction number from DB and add random number from 11-19 to it makes UNIQUE RANDOM Transaction ID
                    long transactionNumber;
                    try { transactionNumber = _context.ReservationDetails.Max(res => res.TransactionNumber) + random.Next(11, 19); }
                    catch { transactionNumber = 798648310; }


                    //Same as Transaction Id Generation
                    int pnr;
                    try { pnr = _context.ReservationDetails.Max(res => res.PnrNumber) + random.Next(11, 19); }
                    catch { pnr = 546781230; }


                    //Same as Transaction Id Generation
                    long seat;
                    try { seat = _context.ReservationDetails.Max(res => res.SeatNumber); }
                    catch { seat = 0; }



                    train = updateSeates(train, reservation.QuotaName, passengers.Count);

                    if (train == null) return null;

                    foreach (var pass in passengers)
                    {
                        ++seat;
                        ReservationDetail reserve = new ReservationDetail()
                        {
                            Train = train,
                            User = user,
                            Passenger = pass,
                            PnrNumber = pnr,
                            Status = "Confirmed",
                            TotalFare = (decimal)(totalFare * 1.00),
                            TransactionNumber = transactionNumber,
                            QuotaName = reservation.QuotaName,
                            SeatNumber = seat,
                            BookingDate = DateTime.Now
                        };
                        passengerTickets.Add(new PassengerTicket { Name = pass.Name, Age = pass.Age, Gender = pass.Gender, Phone = pass.PhoneNumber, Seat = reserve.SeatNumber });

                        _context.ReservationDetails.Add(reserve);
                        _context.SaveChanges();
                        reservations.Add(reserve);

                    }


                    Ticket ticket = new Ticket()
                    {
                        TrainNumber = train.TrainNumber,
                        TrainName = train.TrainName,
                        Pnr = pnr,
                        BookingDate = reservations[0].BookingDate,
                        SourceStation = train.SourceStation,
                        DestinationStation = train.DestinationStation,
                        JourneyStartDate = train.JourneyStartDate,
                        JourneyEndDate = train.JourneyEndDate,
                        TotalFare = (decimal)(totalFare * 1.00),
                        TransactionNumber = transactionNumber,
                        TicketStatus = reservations[0].Status,
                        QuotaName = reservations[0].QuotaName,
                        PassengerTicket = passengerTickets,
                    };

                    return ticket;
                }
                catch (Exception error)
                {
                    _log.LogError(error.Message);
                }

            }

            return null;
        }
        
        private Train updateSeates(Train train, string quota, int seat)
        {
            switch (quota)
            {
                case "General":
                case "general":
                    if (train.AvailableGeneralSeats < seat) return null;
                    train.AvailableGeneralSeats -= seat;

                    break;
                case "Ladies":
                case "ladies":
                    if (train.AvailableLadiesSeats < seat) return null;
                    train.AvailableLadiesSeats -= seat;
                    break;
            }

            return train;
        }

        public Ticket GetTicket(int pnr, string role, string userId)
        {
            Ticket ticket = new Ticket();
            List<ReservationDetail> reservations = new List<ReservationDetail>();
            if (role == "Admin")
            {
                reservations = _context.ReservationDetails.Where(reservation => reservation.PnrNumber == pnr).ToList();
            }
            else if (role == "User")
            {
                reservations = _context.ReservationDetails.Where(reservation => reservation.PnrNumber == pnr && reservation.UserId == userId).ToList();
            }

            if (reservations.Count == 0)
            {
                return null;
            }
            List<PassengerTicket> passengers = new List<PassengerTicket>();

            foreach (var re in reservations)
            {
                List<PassengerDetail> pass = _context.PassengerDetails.Where(passenger => passenger.PassengerId == re.PassengerId).ToList();
                if (pass.Count > 0)
                    passengers.Add(new PassengerTicket { Name = pass[0].Name, Age = pass[0].Age, Gender = pass[0].Gender, Phone = pass[0].PhoneNumber, Seat = re.SeatNumber });
            }



            if (reservations.Count > 0)
            {
                Train train = _context.Trains.Find(reservations[0].TrainId);

                ticket.TrainNumber = train.TrainNumber;
                ticket.TrainName = train.TrainName;
                ticket.Pnr = reservations[0].PnrNumber;
                ticket.BookingDate = reservations[0].BookingDate;
                ticket.SourceStation = train.SourceStation;
                ticket.DestinationStation = train.DestinationStation;
                ticket.JourneyStartDate = train.JourneyStartDate;
                ticket.JourneyEndDate = train.JourneyEndDate;
                ticket.TotalFare = reservations[0].TotalFare;
                ticket.TransactionNumber = reservations[0].TransactionNumber;
                ticket.TicketStatus = reservations[0].Status;
                ticket.QuotaName = reservations[0].QuotaName;
                ticket.PassengerTicket = passengers;
            }

            return ticket;
        }
        
        public Ticket CancelTicket(int pnr)
        {
            List<ReservationDetail> reservations = _context.ReservationDetails.Where(reservation => reservation.PnrNumber == pnr && reservation.Status != "Cancelled").ToList();

            Train train = new Train();
            try { train = _context.Trains.Find(reservations[0].TrainId); }
            catch { return null; }


            //Reservation Only can be cancelled if the Train date is not the same day or past.
            if (DateTime.Now >= train.JourneyStartDate)
            {
                return null;
            }

            foreach (ReservationDetail reservation in reservations)
            {

                reservation.ReservationId = reservation.ReservationId;
                reservation.PassengerId = reservation.PassengerId;
                reservation.TrainId = reservation.TrainId;
                reservation.UserId = reservation.UserId;
                reservation.PnrNumber = reservation.PnrNumber;
                reservation.TransactionNumber = reservation.TransactionNumber;
                reservation.TotalFare = reservation.TotalFare;
                reservation.Status = "Cancelled";
                reservation.QuotaName = reservation.QuotaName;
                reservation.SeatNumber = reservation.SeatNumber;

                _context.Entry(reservation).State = EntityState.Modified;
                _context.SaveChanges();
            }


            switch (reservations[0].QuotaName)
            {
                case "General":
                case "general":
                    train.AvailableGeneralSeats += reservations.Count;

                    break;
                case "Ladies":
                case "ladies":

                    train.AvailableLadiesSeats += reservations.Count;
                    break;
            }

            _context.Entry(train).State = EntityState.Modified;
            _context.SaveChanges();

            return GetTicket(pnr, "Admin","0");
        }

        public bool DeleteReservation(int pnr)
        {
            var reservations = _context.ReservationDetails.Where(r => r.PnrNumber == pnr).ToList();

            if (!reservations.Any()) return false;

            try
            {
                foreach (var reservation in reservations)
                {
                    _context.ReservationDetails.Remove(reservation);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                _log.LogError(error.Message);
                return false;
            }
        }


    }
}