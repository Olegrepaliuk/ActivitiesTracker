using ActivitiesTrackerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitiesTrackerAPI.Models
{
    public class ProjectService
    {
        private ProjectRepository projectRepository;
        private TrackingRepository trackingRepository;
        public ProjectService(ProjectRepository projectRepo, TrackingRepository trackingRepo)
        {
            projectRepository = projectRepo;
            trackingRepository = trackingRepo;
        }
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await projectRepository.GetAllProjects();
        }
        public async Task<Project> GetProjectById(int projId)
        {
            var project = await projectRepository.GetProjectById(projId);
            if (project != null)
            {
                return project;
            }
            else
            {
                throw new ArgumentException($"Unable to find project with id {projId}");
            }
        }
        public async Task CreateProject(Project project)
        {
            await projectRepository.AddProject(project);
        }
        public async Task UpdateProject(int id, Project project)
        {
            if (id == project.Id)
            {
                var foundProject = await projectRepository.GetProjectById(id);
                if (foundProject != null)
                {
                    await projectRepository.UpdateProject(foundProject, project);
                }
                else
                {
                    throw new ArgumentException($"Unable to find project with id {id}");
                }
            }
            else
            {
                throw new FormatException("Identificators do not match");
            }
        }
        public async Task DeleteProject(int projId)
        {
            var project = await projectRepository.GetProjectById(projId);
            if (project != null)
            {
                var trackings = await trackingRepository.GetTrackingsOfProject(projId);
                if(trackings.Count() > 0)
                {
                    throw new InvalidOperationException("Unable to delete project because it is recorded in trackings");
                }
                await projectRepository.RemoveProject(project);
            }
            else
            {
                throw new ArgumentException($"Unable to find project with id {projId}");
            }
        }
    }
}
