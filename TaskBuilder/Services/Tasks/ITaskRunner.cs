using CMS;

using TaskBuilder.Services.Tasks;
using TaskBuilder.Tasks;

[assembly: RegisterImplementation(typeof(ITaskRunner), typeof(TaskRunner), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Tasks
{
    public interface ITaskRunner
    {
        void RunTask(Task task, bool runImmediately);
    }
}