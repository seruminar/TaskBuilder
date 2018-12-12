using System;
using System.Collections.Generic;
using System.Reflection;

using TaskBuilder.Attributes;

namespace TaskBuilder
{
    public class FunctionModel
    {
        public string Name { get; }

        public string Enter { get; }

        public string Leave { get; }

        public ICollection<string> Inputs { get; }

        public ICollection<string> Outputs { get; }

        public FunctionModel(Type function)
        {
            Name = function.Name;
            Inputs = new List<string>();
            Outputs = new List<string>();

            foreach (var method in function.GetMethods())
            {
                if (method.GetCustomAttribute<EnterAttribute>() != null)
                {
                    Enter = method.Name;
                }
            }

            foreach (var property in function.GetProperties())
            {
                if (property.GetCustomAttribute<InputAttribute>() != null)
                {
                    Inputs.Add(property.Name);
                    continue;
                }

                if (property.GetCustomAttribute<LeaveAttribute>() != null)
                {
                    Leave = property.Name;
                    continue;
                }

                if (property.GetCustomAttribute<OutputAttribute>() != null)
                {
                    Outputs.Add(property.Name);
                }
            }
        }
    }
}