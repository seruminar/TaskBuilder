﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using CMS.Core;

using TaskBuilder.Attributes;

namespace TaskBuilder.Services
{
    public class FunctionDiscoveryService : IFunctionDiscoveryService
    {
        private IDictionary<string, Type> _functionTypes;

        public Type GetFunctionType(string functionFullName)
        {
            return _functionTypes[functionFullName];
        }

        public IEnumerable<Type> DiscoverFunctionTypes()
        {
            // Get loaded assemblies
            var discoveredAssemblies = AssemblyDiscoveryHelper.GetAssemblies(false);

            var functionTypes = new List<Type>();

            if (discoveredAssemblies.Any())
            {
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

                    foreach (Type type in assemblyClassTypes.Where(x => x != null))
                    {
                        if (Attribute.IsDefined(type, typeof(FunctionAttribute)))
                            functionTypes.Add(type);
                    }
                }
            }

            _functionTypes = functionTypes.ToDictionary(t => t.FullName);

            return functionTypes;
        }
    }
}