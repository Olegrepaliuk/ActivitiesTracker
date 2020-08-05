using ActivitiesTrackerEntities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitiesTrackerAPI.Models
{
    public class TrackingService
    {
        private TrackingRepository repository;
        public TrackingService(TrackingRepository repo)
        {
            repository = repo;
        }
        public async Task <IEnumerable<Tracking>> GetAllTrackings()
        {
            return await repository.GetAllTrackings();
        }
        public async Task<IEnumerable<Tracking>> GetPersonTrackingsOfDay(int personId, DateTime date)
        {
            return await repository.GetPersonTrackingsOfDay(personId, date);
        }

        public async Task<IEnumerable<Tracking>> GetPersonTrackingsOfWeek(int year, int personId, int weekNum)
        {
            DateTime startDate;
            DateTime endDate;
            var possibleStartDate = ISOWeek.ToDateTime(year, weekNum, DayOfWeek.Monday);
            if (possibleStartDate.Year > year)
            {
                throw new ArgumentException($"Week {weekNum} doesn`t exist in this year");
            }
            if (possibleStartDate.Year < year)
            {
                possibleStartDate = new DateTime(year, 1, 1);
            }
            startDate = possibleStartDate;
            int daysToEnd = 7 - (int)startDate.DayOfWeek;
            var possibleEndDate = startDate.AddDays(daysToEnd);
            if(possibleEndDate.Year > startDate.Year)
            {
                endDate = new DateTime(startDate.Year, 12, 31);
            }
            else
            {
                endDate = possibleEndDate;
            }
            return await repository.GetPersonTrackingsOfDateRange(personId, startDate, endDate);
        }
    }
}
