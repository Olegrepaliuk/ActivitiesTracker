using ActivitiesTrackerEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitiesTrackerAPI.Models
{
    public class TrackerContext : DbContext
    {
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles{ get; set; }
        public DbSet<Tracking> Trackings { get; set; }
        public TrackerContext(DbContextOptions<TrackerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
