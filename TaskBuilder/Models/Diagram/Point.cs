using System;

namespace TaskBuilder.Models.Diagram
{
    public class Point
    {
        public Guid Id { get; set; }

        public bool Selected { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    }
}