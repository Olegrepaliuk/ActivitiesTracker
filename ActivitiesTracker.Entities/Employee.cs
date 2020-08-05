using System;

namespace ActivitiesTrackerEntities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
