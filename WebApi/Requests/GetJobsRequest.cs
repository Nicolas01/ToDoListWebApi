using WebApi.Models;

namespace WebApi.Requests;

public record GetJobsRequest(
    string? Title,
    JobStatus? JobStatus,
    byte Limit = 2);
