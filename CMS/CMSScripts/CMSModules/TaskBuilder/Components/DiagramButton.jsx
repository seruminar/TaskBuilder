class DiagramButton extends React.Component {
    state = {
        diagram: null,
        onClickHandler: null
    };

    onClickHandler(e) {
        e.preventDefault();
        this.state.onClickHandler(this.state.diagram, e);
    }

    render() {
        return (
            <button onClick={e => this.onClickHandler(e)}>
                <i className="cms-icon-80 icon-square" />
                <br />
                <span>{this.props.text}</span>
            </button>
        );
    }
}