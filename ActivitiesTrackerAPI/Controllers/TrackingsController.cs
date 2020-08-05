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
    public class TrackingsController : ControllerBase
    {
        private TrackingService trackingService;
        private readonly ILogger<TrackingsController> _logger;
        public TrackingsController(TrackingService service, ILogger<TrackingsController> logger)
        {
            trackingService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tracking>>> GetAll()
        {
            var allTrackings = await trackingService.GetAllTrackings();
            _logger.LogInformation("Handled request to receive all trackings info");
            return allTrackings.ToList();
        }

        [HttpGet("personAndDay")]
        public async Task <ActionResult<IEnumerable<Tracking>>> GetTrackingsByPersonAndDay([FromBody] PersonDayModel model)
        {
            var trackings = await trackingService.GetPersonTrackingsOfDay(model.PersonId, model.Date);
            _logger.LogInformation($"Handled request to receive info about person {model.PersonId} daily activities");
            return trackings.ToList();
        }

        [HttpGet("personAndYearWeek")]
        public async Task<ActionResult<IEnumerable<Tracking>>> GetTrackingsByPersonAndYearWeek([FromBody] PersonYearWeekModel model)
        {
            try
            {
                var trackings = await trackingService.GetPersonTrackingsOfWeek(model.Year, model.PersonId, model.WeekNum);
                _logger.LogInformation($"Handled request to receive info about person {model.PersonId} activities in {model.Year} on {model.WeekNum} week");
                return trackings.ToList();
            }
            catch(ArgumentException e)
            {
                _logger.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}