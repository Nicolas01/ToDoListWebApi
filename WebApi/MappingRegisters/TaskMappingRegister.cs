using Mapster;
using WebApi.Requests;
using Job = WebApi.Models.Job;

namespace WebApi.MappingRegisters;

public class TaskMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config) => config.NewConfig<CreateUpdateJobRequest, Job>();
}
