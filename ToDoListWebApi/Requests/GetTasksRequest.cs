using TaskStatus = ToDoListWebApi.Models.TaskStatus;

namespace ToDoListWebApi.Requests;

public record GetTasksRequest(
    string? Title,
    TaskStatus? TaskStatus,
    byte Limit = 2);
