using System;
using TaskBuilder.Attributes;
using TaskBuilder.Models;

namespace TaskBuilder.Actions
{
    public class StartAction : TaskAction
    {
        private string _sourceOutParameter;

        // Encapsulates behavior of action
        [InReceiver]        
        public void SourceInReceiver()
        {
            // Receive parameters

            // Set up parameters
            _sourceOutParameter = "source string";

            // Send
            SourceOutSender();
        }

        // This must be linked as a imperative delegate
        [OutSender]
        public Action SourceOutSender { get; set; }

        [OutParameter]
        public string SourceOutParameter()
        {
            return _sourceOutParameter;
        }

        public StartAction(Node node) : base(node)
        {
        }
    }
}