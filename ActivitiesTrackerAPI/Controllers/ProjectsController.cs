using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesTrackerAPI.Models;
using ActivitiesTrackerEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ActivitiesTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private ProjectService projectService;
        private readonly ILogger<ProjectsController> _logger;
        public ProjectsController(ProjectService service, ILogger<ProjectsController> logger)
        {
            projectService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll()
        {           
            var allProjects = await projectService.GetAllProjects();
            _logger.LogInformation("Handled request to receive all projects info");
            return allProjects.ToList();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get(int id)
        {
            try
            {
                var foundProject = await projectService.GetProjectById(id);
                _logger.LogInformation($"Handled request to receive info about project with id {id}");
                return foundProject;
            }
            catch(ArgumentException e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Project proj)
        {
            await projectService.CreateProject(proj);
            _logger.LogInformation("Handled request to create new project");
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]Project proj)
        {
            try
            {
                await projectService.UpdateProject(id, proj);
                _logger.LogInformation($"Handled request to update project with id {id}");
                return NoContent();
            }
            catch(FormatException fe)
            {
                _logger.LogError(fe.Message);
                return BadRequest(fe.Message);
            }
            catch(ArgumentException ae)
            {
                _logger.LogError(ae.Message);
                return NotFound(ae.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await projectService.DeleteProject(id);
                _logger.LogInformation($"Handled request to delete project with id {id}");
                return NoContent();
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
            catch(InvalidOperationException ioe)
            {
                _logger.LogError(ioe.Message);
                return BadRequest(ioe.Message);
            }
        }
    }
}