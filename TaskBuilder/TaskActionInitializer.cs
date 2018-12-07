using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CMS.Base;
using CMS.Core;
using CMS.EventLog;
using TaskBuilder.Actions;
using TaskBuilder.Attributes;

namespace TaskBuilder
{
    internal class TaskActionInitializer : AbstractWorker
    {
        public override void Run()
        {
            // Get loaded assemblies
            var discoveredAssemblies = AssemblyDiscoveryHelper.GetAssemblies(false);

            var actionTypes = new List<Type>();

            foreach (var assembly in discoveredAssemblies)
            {
                // Get list of classes from selected assembly
                Type[] assemblyClassTypes;
                try
                {
                    assemblyClassTypes = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException exception)
                {
                    assemblyClassTypes = exception.Types;
                }

                actionTypes.AddRange(assemblyClassTypes.Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(TaskAction)))
                                                           //.Select(t => (TaskAction)Activator.CreateInstance(t))
                                                           );
            }

            if (!actionTypes.Any())
            {
                EventLogProvider.LogInformation(nameof(TaskActionInitializer), "NOACTIONS", "TaskActionInitializer could not find any actions.");
            }

            foreach (var actionType in actionTypes)
            {
                RegisterActionType(actionType);
            }
        }

        private void RegisterActionType(Type action)
        {
            // Find all of the port methods and create an object in memory (or DB?) that represents its structure
            // When a task builder is opened, the React app pulls in the structures for deserialization 
            //                                                                          and creating new ones in the side drawer

            foreach (var method in action.GetMethods())
            {
                method.GetCustomAttributes(typeof(InReceiverAttribute));
            }
            //action.GetMethods()
            //    .Where(m => m.GetCustomAttributes<InReceiverAttribute>(false).Any())

        }
    }
}
