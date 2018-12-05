using System;
using TaskBuilder.Attributes;
using TaskBuilder.Models;
using Port = TaskBuilder.Attributes.Port;

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
            var source = new StartAction(new Node());
            var target = new StartAction(new Node());

            // Connect links
            source.SourceOutSender = target.TargetInReceiver;
            target.TargetInParameter = source.SourceOutParameter;

            // Call start node
            source.SourceInReceiver();
        }
    }
}