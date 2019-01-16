class DiagramToast extends React.Component {
    state = {
        show: false,
        message: null
    }

    componentDidUpdate() {
        setTimeout(() => {
            this.setState({
                show: false,
                message: null
            });
        }, 3000);
    }

    render() {
        return (
            <div className="task-builder-toast alert alert-success"
                style={{ display: this.state.show ? "block" : "none" }}
            >
                <span className="alert-icon">
                    <i className="icon-check-circle" />
                    <span className="sr-only">Success</span>
                </span>
                <span className="alert-label">{this.state.message}</span>
            </div>
        );
    }
}