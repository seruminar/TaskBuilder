class DiagramToast extends React.Component {
    state = {
        show: false,
        result: null,
        message: null
    }

    componentDidUpdate() {
        if (this.state.show) {
            clearTimeout(this.timeout);

            this.timeout = setTimeout(() => {
                this.setState({
                    show: false,
                    result: null,
                    message: null
                });
            }, 3000);
        }
    }

    timeout;

    render() {
        return (
            <div className={`task-builder-toast alert alert-${this.state.result}`}
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