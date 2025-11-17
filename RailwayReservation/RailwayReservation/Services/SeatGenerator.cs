using RailwayReservation.Models;

namespace OnlineRailwayReservationSystemAPI.Services
{
    public class SeatGenerator
    {
        OnlineRailwayReservationSystemDbContext _context = null;
        public SeatGenerator(OnlineRailwayReservationSystemDbContext context)
        {
            _context = context;
        }

        public bool AddTrainSeats(string trainId, List<string> classIds)
        {
            bool areSeatsAdded = false;
            // iterate through the classIds 
            foreach (var classId in classIds)
            {
                //generate a new TrainClass object which stores train Id and their corresponding coach Ids
                TrainClass newTrainClass = new TrainClass()
                {
                    TrainId = trainId,
                    ClassId = classId
                };
                _context.TrainClasses.Add(newTrainClass);
                _context.SaveChanges();

            }

            //retreive trainClassIds from the above table as async list from db
            var trainClassList = _context.TrainClasses.Where(tc => tc.TrainId == trainId).Select(tc => new { tc.TrainClassId, tc.ClassId }).ToList();

            //for each trainClassId generate a new ClassCoach with corresponding coach id
            foreach(var trainClass in trainClassList)
            {
                //get coach ids for each trainClassId from the user-defined function GetCoachIds
                string[] coachIds = GetCoachIds(trainClass.ClassId); // returns a string array like : ['CH01', 'CH02'], ['CH03', 'CH04']

                //for each coachId add object add classcoach object
                foreach(var coachId in coachIds)
                {
                    ClassCoach classCoach = new ClassCoach()
                    {
                        TrainClassId = trainClass.TrainClassId,
                        CoachId = coachId
                    };

                    _context.ClassCoaches.Add(classCoach);
                    _context.SaveChanges();

                    //generating 64 seats for each CoachClassId 
                    for (int i = 1; i <= 64; i++)
                    {
                        var seat = new Seat
                        {
                            //SeatId = ",
                            Quota = i <= 6 ? "Ladies" : "General",
                            SeatNumber = i, // Example seat number format
                            ClassCoachId = classCoach.ClassCoachId,
                            AvailabilityStatus = true
                        };
                        _context.Seats.Add(seat);
                        _context.SaveChanges();
                        
                    }
                    
                }

                areSeatsAdded =  true;
            }

            return areSeatsAdded;
        }

        //generating coachId for each class based on classId and coachId
        public string[] GetCoachIds(string classId)
        {
            string[] coachIds = new string[2];
            switch (classId)
            {
                case "CL101":
                    {
                        coachIds = ["CH01", "CH02"];
                        break;
                    }
                case "CL102":
                    {
                        coachIds = ["CH03", "CH04"];
                        break;
                    }
                case "CL103":
                    {
                        coachIds = ["CH05", "CH06"];
                        break;
                    }
                case "CL104":
                    {
                        coachIds = ["CH07", "CH08"];
                        break;
                    }
                case "CL105":
                    {
                        coachIds = ["CH09", "CH10"];
                        break;
                    }
                case "CL106":
                    {
                        coachIds = ["CH11", "CH12"];
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return coachIds;
        }
    }
}
