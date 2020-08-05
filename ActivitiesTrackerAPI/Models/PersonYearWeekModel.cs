using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitiesTrackerAPI.Models
{
    public class PersonYearWeekModel
    {
        public int PersonId { get; set; }
        public int Year { get; set; }
        public int WeekNum { get; set; }
    }
}
