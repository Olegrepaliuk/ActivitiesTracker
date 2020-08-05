using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ActivitiesTrackerEntities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        [GreaterThan("DateStart")]
        public DateTime DateEnd { get; set; }
    }
}
