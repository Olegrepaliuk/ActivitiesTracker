using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesTrackerEntities
{
    public class Tracking
    {
        public int Id { get; set; }
        public Employee Person { get; set; }
        public int? PersonId { get; set; }
        public Role PersonRole { get; set; }
        public int? PersonRoleId { get; set; }
        public Project Proj { get; set; }
        public int? ProjId { get; set; }
        public ActivityType TypeOfActivity { get; set; }
        public int? TypeOfActivityId { get; set; }
        public DateTime ActivityStart { get; set; }
        [GreaterThan("ActivityStart")]
        public DateTime ActivityEnd { get; set; }
    }
}
