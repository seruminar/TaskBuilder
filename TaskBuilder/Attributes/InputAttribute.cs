﻿using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property,
               AllowMultiple = false,
               Inherited = true)]
    public class InputAttribute : BaseFunctionAttribute
    {
        public InputAttribute()
        {
        }
    }
}