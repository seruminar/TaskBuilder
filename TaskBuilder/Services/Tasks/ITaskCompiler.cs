using CMS;

using TaskBuilder.Models.Diagram;
using TaskBuilder.Services.Tasks;
using TaskBuilder.Tasks;

[assembly: RegisterImplementation(typeof(ITaskCompiler), typeof(TaskCompiler), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Tasks
{
    public interface ITaskCompiler
    {
        Task PrepareTask(Diagram diagram);
    }
}