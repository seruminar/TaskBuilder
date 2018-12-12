using React;

namespace TaskBuilder
{
    public static class DiagramFactory
    {
        public static IReactEnvironment Environment => ReactEnvironment.GetCurrentOrThrow;
        public static IReactComponent GetReactComponent(string componentName, object props = null)
        {
            props = props ?? new { };

            var reactComponent = Environment.CreateComponent(componentName, props, clientOnly: true);

            return reactComponent;
        }
    }
}