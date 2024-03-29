﻿using System;
using System.Drawing;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
    public class FunctionAttribute : Attribute
    {
        internal string DisplayName { get; }

        internal Color DisplayColor { get; }

        public FunctionAttribute(string displayName = null)
        {
            DisplayName = displayName;
        }

        public FunctionAttribute(int r, int g, int b) : this(255, r, g, b)
        {
        }

        public FunctionAttribute(int a, int r, int g, int b)
        {
            DisplayColor = Color.FromArgb(a, r, g, b);
        }

        public FunctionAttribute(string displayName, int r, int g, int b) : this(displayName, 255, r, g, b)
        {
        }

        public FunctionAttribute(string displayName, int a, int r, int g, int b)
        {
            DisplayName = displayName;
            DisplayColor = Color.FromArgb(a, r, g, b);
        }
    }
}