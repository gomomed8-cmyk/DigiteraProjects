using JobApplication.Application.DTOs;
using JobApplication.Application.Features.Jobs.Commands.CreateJob;
using JobApplication.Application.Features.Jobs.Queries.GetAllJobs;
using JobApplication.Application.Features.Jobs.Queries.GetJobById;
using JobApplication.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    /// <summary>
    /// Manages job postings.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        //private readonly IJobService _JobService;
        private readonly IMediator _mediator;

        public JobsController( IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all job postings.
        /// </summary>
        /// <returns>The list of jobs.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            //var jobs = _JobService.GetAll();
            var jobs = _mediator.Send(new GetAllJobsQuery());
            return Ok(new { jobs });
        }

        /// <summary>
        /// Gets a single job posting by its id.
        /// </summary>
        /// <param name="id">The job id.</param>
        /// <returns>The matching job.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            //var job = _JobService.GetById(id);
            var job = _mediator.Send(new GetJobByIdQuery() { Id = id});
            if (job is null) return NotFound(new
            {
                message = "invalid Id"
            });
            return Ok(new { job });
        }

        /// <summary>
        /// Creates a new job posting.
        /// </summary>
        /// <param name="createJobDto">The job title and description.</param>
        /// <returns>The id of the newly created job.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateJobDto createJobDto)
        {
            //var id = await _JobService.CreateAsync(createJobDto);
            var id = await _mediator.Send(new CreateJobCommand() { Title = createJobDto.Title  , Description = createJobDto.Description });

            return Ok(new
            {
                id = id
            });
        }
    }
}
