using TaskBuilder.Tasks;

namespace TaskBuilder.Services.Tasks
{
    internal class TaskRunner : ITaskRunner
    {
        public void RunTask(Task task, bool runImmediately)
        {
            task.Invoke();
        }
    }
}