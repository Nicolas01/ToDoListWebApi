using TaskStatus = ToDoListWebApi.Models.TaskStatus;

namespace ToDoListWebApi.Requests;

public record CreateUpdateTaskRequest(
    string Title,
    TaskStatus TaskStatus);
