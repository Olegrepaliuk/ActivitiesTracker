using ActivitiesTrackerEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitiesTrackerAPI.Models
{
    public class TrackingRepository
    {
        private TrackerContext db;
        public TrackingRepository(TrackerContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Tracking>> GetAllTrackings()
        {
            var trackings = await db.Trackings.Include(tr => tr.Person).Include(tr => tr.Proj).ToListAsync();
            return trackings;
        }
        public async Task<IEnumerable<Tracking>> GetPersonTrackingsOfDay(int personId, DateTime date)
        {
            var trackings = await db.Trackings.Include(tr => tr.Person)
                                    .Include(tr => tr.Proj)
                                    .Where(tr => (tr.PersonId == personId)&&((tr.ActivityStart.Date == date.Date)||(tr.ActivityEnd.Date == date.Date)))
                                    .ToListAsync();
            return trackings;
        }

        public async Task<IEnumerable<Tracking>> GetPersonTrackingsOfDateRange(int personId, DateTime startWeekDate, DateTime endWeekDate)
        {
            var trackings = await db.Trackings.Include(tr => tr.Person)
                                        .Include(tr => tr.Proj)
                                        .Where(tr => tr.PersonId == personId)
                                        .Where(tr => (tr.ActivityStart.Date >= startWeekDate.Date && tr.ActivityStart.Date <= endWeekDate.Date) ||
                                        (tr.ActivityEnd.Date >= startWeekDate.Date && tr.ActivityEnd.Date <= endWeekDate.Date))
                                        .ToListAsync();
            return trackings;
        }
        public async Task<IEnumerable<Tracking>> GetTrackingsOfProject(int projId)
        {
            var trackings = await db.Trackings.Where(tr => tr.ProjId == projId).ToListAsync();
            return trackings;
        }
    }
}
