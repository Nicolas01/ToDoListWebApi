using System.Text.Json.Serialization;

namespace ToDoListWebApi.Models;

public class Task
{
    public uint Id { get; set; }
    public required string Title { get; set; }
    public TaskStatus TaskStatus { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter<TaskStatus>))]
public enum TaskStatus
{
    ReadyToStart,
    InProgress,
    Completed,
    Cancelled,
    Deleted
}
