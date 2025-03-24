using LinqKit;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Requests;
using JobStatus = WebApi.Models.JobStatus;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class JobsController : ControllerBase
{
    private static readonly List<Job> jobs =
    [
        new Job { Id = 1, Title = "Job 1", JobStatus = JobStatus.ReadyToStart },
        new Job { Id = 2, Title = "Job 2", JobStatus = JobStatus.InProgress },
        new Job { Id = 3, Title = "Job 3", JobStatus = JobStatus.Completed }
    ];

    [HttpGet]
    [ProducesResponseType<IEnumerable<Job>>(StatusCodes.Status200OK)]
    [EndpointSummary("Get jobs")]
    public IEnumerable<Job> Get([FromQuery] GetJobsRequest getJobsRequest)
    {
        var predicate = PredicateBuilder.New<Job>(true);

        if (getJobsRequest.Title is not null)
            predicate.And(x => x.Title.Contains(getJobsRequest.Title));

        if (getJobsRequest.JobStatus is not null)
            predicate.And(x => x.JobStatus == getJobsRequest.JobStatus.Value);

        return jobs
            .Where(predicate)
            .Take(getJobsRequest.Limit);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("Get a job by its id")]
    public ActionResult<Job> Get(uint id)
    {
        var job = jobs.Find(x => x.Id == id);
        return job is null ? NotFound() : Ok(job);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [EndpointSummary("Create a new job")]
    public ActionResult<Job> Post(CreateUpdateJobRequest createUpdateJobRequest)
    {
        var newJob = createUpdateJobRequest.Adapt<Job>();
        newJob.Id = jobs.Max(x => x.Id) + 1;
        jobs.Add(newJob);

        return CreatedAtRoute(new { newJob.Id }, newJob);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("Replace a job by its id")]
    public IActionResult Put(uint id, CreateUpdateJobRequest createUpdateJobRequest)
    {
        var jobToUpdate = jobs.Find(x => x.Id == id);
        if (jobToUpdate is null)
            return NotFound();

        createUpdateJobRequest.Adapt(jobToUpdate);

        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("Update a job by its id")]
    public IActionResult Patch(uint id, JsonPatchDocument<Job> jobPatchDocument)
    {
        if (jobPatchDocument is null)
            return BadRequest();

        var jobToUpdate = jobs.Find(x => x.Id == id);
        if (jobToUpdate is null)
            return NotFound();

        jobPatchDocument.ApplyTo(jobToUpdate, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("Delete a job by its id")]
    public IActionResult Delete(uint id)
    {
        var job = jobs.Find(x => x.Id == id);
        if (job is null)
            return NotFound();

        jobs.Remove(job);
        return NoContent();
    }
}
