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
            return (
                <div style={{ padding: '10px 15px' }}>
                    <h2>Something went wrong.</h2>
                    <div style={{ whiteSpace: 'pre-wrap' }}>
                        {this.state.error.stack}
                        <br />
                        {this.state.errorInfo.componentStack}
                    </div>
                    <br />
                    <h4>Props:</h4>
                    <pre>{JSON.stringify(this.props, null, 4)}</pre>
                </div>
            );
        }

        return <TaskDiagram {...this.props} />;
    }
}