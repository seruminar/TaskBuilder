class DiagramButton extends React.Component {
    render() {
        return (
            <button type="button" onClick={this.props.onClick}>
                <i className={"cms-icon-80 " + this.props.iconClass} />
                <br />
                <span>{this.props.text}</span>
            </button>
        );
    }
}