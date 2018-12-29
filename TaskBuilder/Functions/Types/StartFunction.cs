using System;

using TaskBuilder.Attributes;

namespace TaskBuilder.Functions
{
    [Function("Start")]
    public class StartFunction
    {
        // Encapsulates behavior of Function
        [Enter]
        public void SourceInReceiver()
        {
            // Receive parameters

            // Set up parameters
            SourceOutParameter = "source string";

            // Send / Execute
            SourceOutSender();
        }

        // This must be linked as a imperative delegate
        [Leave]
        public Action SourceOutSender { get; set; }

        [Output]
        public string SourceOutParameter { get; set; }
    }
}