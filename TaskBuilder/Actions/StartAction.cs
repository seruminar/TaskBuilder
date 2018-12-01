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
        public void SourceInPortReactive()
        {
            // Receive parameters

            // Set up parameters
            SourceOutPortParameter = () => "source string";

            // Send imperative
            SourceOutPortImperative();
        }

        // This must be linked as a imperative delegate
        [OutSender]
        public Action SourceOutPortImperative { get; set; }

        [OutParameter]
        public Func<string> SourceOutPortParameter { get; set; }

        [InReceiver]
        public void TargetInPortReactive()
        {
            // Receive parameters
            var fromSource = TargetInPortParameter();

            // Set up parameters


            // Send imperative

        }

        // This must be linked as a reactive parameter
        [InParameter]
        public Func<string> TargetInPortParameter { get; set; }

        public StartAction(Node node) : base(node)
        {
            var source = new StartAction(new Node());
            var target = new StartAction(new Node());

            // Connect links
            source.SourceOutPortImperative = target.TargetInPortReactive;
            target.TargetInPortParameter = source.SourceOutPortParameter;

            // Call start node
            source.SourceInPortReactive();
        }
    }
}