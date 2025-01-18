using Mapster;
using ToDoListWebApi.Requests;
using Task = ToDoListWebApi.Models.Task;

namespace ToDoListWebApi.MappingRegisters;

public class TaskMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config) => config.NewConfig<CreateUpdateTaskRequest, Task>();
}
