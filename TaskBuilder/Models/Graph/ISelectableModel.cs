using System;

namespace TaskBuilder.Models.Graph
{
    public interface ISelectableModel
    {
        Guid Id { get; set; }

        bool Selected { get; set; }
    }
}