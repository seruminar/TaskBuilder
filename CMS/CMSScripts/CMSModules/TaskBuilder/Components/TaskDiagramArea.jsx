class TaskDiagramArea extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            hasError: false,
            error: null,
            errorInfo: null
        };
    }

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
            return (
                <div style={{ padding: '10px 15px'}}>
                    <h2>Something went wrong.</h2>
                    <div style={{ whiteSpace: 'pre-wrap' }}>
                        {this.state.error && this.state.error.toString()}
                        <br/>
                        {this.state.errorInfo.componentStack}
                    </div>
                    <br/>
                    <h4>Props:</h4>
                    <pre>{JSON.stringify(this.props, null, 4)}</pre>
                </div>
            );
        }   

        return (
            <TaskDiagram functions={this.props.functions} graph={this.props.graph}/>
        );
    }
}