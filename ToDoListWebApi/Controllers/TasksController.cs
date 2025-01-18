using LinqKit;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ToDoListWebApi.Requests;
using Task = ToDoListWebApi.Models.Task;
using TaskStatus = ToDoListWebApi.Models.TaskStatus;

namespace ToDoListWebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class TasksController : ControllerBase
{
    private static readonly List<Task> tasks =
    [
        new Task { Id = 1, Title = "Task 1", TaskStatus = TaskStatus.ReadyToStart },
        new Task { Id = 2, Title = "Task 2", TaskStatus = TaskStatus.InProgress },
        new Task { Id = 3, Title = "Task 3", TaskStatus = TaskStatus.Completed }
    ];

    [HttpGet]
    [ProducesResponseType<IEnumerable<Task>>(StatusCodes.Status200OK)]
    [EndpointSummary("Get tasks")]
    public IEnumerable<Task> Get([FromQuery] GetTasksRequest getTaskRequest)
    {
        var predicate = PredicateBuilder.New<Task>(true);

        if (getTaskRequest.Title is not null)
            predicate.And(x => x.Title.Contains(getTaskRequest.Title));

        if (getTaskRequest.TaskStatus is not null)
            predicate.And(x => x.TaskStatus == getTaskRequest.TaskStatus.Value);

        return tasks
            .Where(predicate)
            .Take(getTaskRequest.Limit);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Task>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("Get a task by its id")]
    public IActionResult Get(uint id)
    {
        var task = tasks.Find(x => x.Id == id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    [ProducesResponseType<Task>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [EndpointSummary("Create a new task")]
    public IActionResult Post([FromBody] CreateUpdateTaskRequest createUpdateTaskRequest)
    {
        var newTask = createUpdateTaskRequest.Adapt<Task>();
        newTask.Id = tasks.Max(x => x.Id) + 1;
        tasks.Add(newTask);

        return CreatedAtRoute(new { newTask.Id }, newTask);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("Replace a task by its id")]
    public IActionResult Put(uint id, [FromBody] CreateUpdateTaskRequest createUpdateTaskRequest)
    {
        var taskToUpdate = tasks.Find(x => x.Id == id);
        if (taskToUpdate is null)
            return NotFound();

        createUpdateTaskRequest.Adapt(taskToUpdate);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("Delete a task by its id")]
    public IActionResult Delete(uint id)
    {
        var task = tasks.Find(x => x.Id == id);
        if (task is null)
            return NotFound();

        tasks.Remove(task);
        return NoContent();
    }
}
