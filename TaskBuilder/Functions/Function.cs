using System;
using System.Collections.Generic;
using TaskBuilder.Models;

namespace TaskBuilder.Functions
{
    public abstract class Function
    {
        public Guid Guid { get; set; }
    }
}