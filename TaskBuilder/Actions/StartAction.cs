using System;
using TaskBuilder.Attributes;
using TaskBuilder.Models;

namespace TaskBuilder.Actions
{
    public class StartAction : TaskAction
    {
        // Encapsulates behavior of action
        [InReceiver]        
        public void SourceInReceiver()
        {
            // Receive parameters

            // Set up parameters
            SourceOutParameter = () => "source string";

            // Send
            SourceOutSender();
        }

        // This must be linked as a imperative delegate
        [OutSender]
        public Action SourceOutSender { get; set; }

        [OutParameter]
        public Func<string> SourceOutParameter { get; set; }

        [InReceiver]
        public void TargetInReceiver()
        {
            // Receive parameters
            var fromSource = TargetInParameter();

            // Set up parameters

             
            // Send

        }

        // This must be linked as a reactive parameter
        [InParameter]
        public Func<string> TargetInParameter { get; set; }

        public StartAction(Node node) : base(node)
        {
        }
    }
}