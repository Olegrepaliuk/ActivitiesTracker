using ActivitiesTrackerEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitiesTrackerAPI.Models
{
    public class ProjectRepository
    {
        private TrackerContext db;
        public ProjectRepository(TrackerContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            var projects = await db.Projects.ToListAsync();
            return projects;
        }
        public async Task<Project> GetProjectById(int projId)
        {
            var foundproj = await db.Projects.FindAsync(projId);
            return foundproj;
        }

        public async Task AddProject(Project project)
        {
            db.Projects.Add(project);
            await db.SaveChangesAsync();
        }
        public async Task UpdateProject(Project exist, Project newValues)
        {
            db.Entry(exist).CurrentValues.SetValues(newValues);
            await db.SaveChangesAsync();
        }
        public async Task RemoveProject(Project project)
        {
            db.Projects.Remove(project);
            await db.SaveChangesAsync();
        }
    }
}
