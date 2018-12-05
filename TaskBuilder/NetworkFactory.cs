using React;

namespace TaskBuilder
{
    public static class DiagramFactory
    {
        // Alternatively there is AssemblyRegistration.Container.Resolve<IReactEnvironment>()
        public static IReactEnvironment Environment => ReactEnvironment.GetCurrentOrThrow;
        public static IReactComponent GetReactComponent(string componentName, object props = null)
        {
            props = props ?? new { };

            var reactComponent = Environment.CreateComponent(componentName, props, clientOnly: true);

            return reactComponent;
        }
    }
}