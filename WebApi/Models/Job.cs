using System.Text.Json.Serialization;

namespace WebApi.Models;

public class Job
{
    public uint Id { get; set; }
    public required string Title { get; set; }
    public JobStatus JobStatus { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter<JobStatus>))]
public enum JobStatus
{
    ReadyToStart,
    InProgress,
    Completed,
    Cancelled,
    Deleted
}
