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
            var actionInfos = ActionInfoProvider.GetActions();

            foreach (var actionInfo in actionInfos)
            {
                var action = ClassHelper.GetAssembly(actionInfo.ActionBehaviorAssembly).GetType(actionInfo.ActionBehaviorClass);

                if (action != null)
                {
                    RegisterAction(action);
                }
            }
        }

        private void RegisterAction(Type action)
        {
            // Find all of the port methods and create an object in memory (or DB?) that represents its structure
            // When a task builder is opened, the React app pulls in the structures for deserialization 
            //                                                                          and creating new ones in the side drawer

            foreach (var method in action.GetMethods())
            {
                //method.GetCustomAttributes
            }
            //action.GetMethods()
            //    .Where(m => m.GetCustomAttributes<InReceiverAttribute>(false).Any())

        }
    }
}
