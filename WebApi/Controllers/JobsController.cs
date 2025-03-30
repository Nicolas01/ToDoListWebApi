using LinqKit;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Requests;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
    private static readonly List<Job> jobs =
    [
        new Job { Id = 1, Title = "Job 1", JobStatus = JobStatus.ReadyToStart },
        new Job { Id = 2, Title = "Job 2", JobStatus = JobStatus.InProgress },
        new Job { Id = 3, Title = "Job 3", JobStatus = JobStatus.Completed }
    ];

    [HttpGet]
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
    [EndpointSummary("Get a job by its id")]
    public ActionResult<Job> Get(uint id)
    {
        var job = jobs.Find(x => x.Id == id);
        return job is null ? NotFound(id) : Ok(job);
    }

    [HttpPost]
    [EndpointSummary("Create a new job")]
    public ActionResult<Job> Create(CreateUpdateJobRequest createUpdateJobRequest)
    {
        var newJob = createUpdateJobRequest.Adapt<Job>();
        newJob.Id = jobs.Max(x => x.Id) + 1;
        jobs.Add(newJob);

        return CreatedAtAction(nameof(Get), new { id = newJob.Id }, newJob);
    }

    [HttpPut("{id}")]
    [EndpointSummary("Replace a job by its id")]
    public IActionResult Replace(uint id, CreateUpdateJobRequest createUpdateJobRequest)
    {
        var jobToUpdate = jobs.Find(x => x.Id == id);
        if (jobToUpdate is null)
            return NotFound();

        createUpdateJobRequest.Adapt(jobToUpdate);

        return NoContent();
    }

    [HttpPatch("{id}")]
    [EndpointSummary("Update a job by its id")]
    public IActionResult Update(uint id, JsonPatchDocument<Job> jobPatchDocument)
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
