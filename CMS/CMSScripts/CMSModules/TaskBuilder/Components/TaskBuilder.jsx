class TaskBuilder extends React.Component {
    state = {
        hasError: false,
        error: null,
        errorInfo: null
    };

    componentDidCatch(error, info) {
        // Display fallback UI
        this.setState({
            hasError: true,
            error: error,
            errorInfo: info
        });
    }

    render() {
        if (this.state.hasError) {
            console.log(this.state);
            console.log(this.props);
            return (
                <div style={{ padding: '10px 15px' }}>
                    <h2>Something went wrong.</h2>
                    <div style={{ whiteSpace: 'pre-wrap' }}>
                        {this.state.error.stack}
                        <br />
                        {this.state.errorInfo.componentStack}
                    </div>
                </div>
            );
        }

        // Hack to revert version of _ that was loaded by Kentico's cmsrequire
        const setUnderscore = setInterval(() => {
            if (_.VERSION === "1.5.2") {
                _.noConflict();
                clearInterval(setUnderscore);
            }
        }, 50);

        return <TaskDiagram {...this.props} />;
    }
}