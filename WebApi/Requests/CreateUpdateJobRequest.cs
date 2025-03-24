using JobStatus = WebApi.Models.JobStatus;

namespace WebApi.Requests;

public record CreateUpdateJobRequest(
    string Title,
    JobStatus TaskStatus);
