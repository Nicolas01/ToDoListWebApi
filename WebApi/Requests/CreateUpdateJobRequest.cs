using WebApi.Models;

namespace WebApi.Requests;

public record CreateUpdateJobRequest(
    string Title,
    JobStatus TaskStatus);
