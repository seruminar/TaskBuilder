using System;

using CMS.DataEngine;

using TaskBuilder.Attributes;
using TaskBuilder.ValueBuilders;

namespace TaskBuilder.Functions.Implementations
{
    [Function("Get value")]
    public struct GetValueFunction : IInvokable, IDispatcher
    {
        public void Invoke()
        {
            // Receive parameters
            var info = Info();
            var columnName = ColumnName();

            // Set up parameters
            Value = info.GetValue(columnName)?.ToString();

            Dispatch();
        }

        public Action Dispatch { get; set; }

        [Input]
        public Func<BaseInfo> Info { get; set; }

        [Input(typeof(StringValueBuilder), new object[] { "columnName" })]
        public Func<string> ColumnName { get; set; }

        [Output]
        public string Value { get; set; }
    }
}