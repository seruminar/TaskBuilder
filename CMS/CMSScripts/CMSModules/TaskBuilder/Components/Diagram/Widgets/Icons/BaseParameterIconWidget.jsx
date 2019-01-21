const SRD = window["storm-react-diagrams"];

class BaseParameterIconWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port", props);
        this.state = {
            selected: false
        };
    }

    getClassName() {
        return super.getClassName() + (this.state.selected ? this.bem("-selected") : "");
    }

    render() {
        return (
            <div className="port-icon">
                <div
                    {...this.getProps()}
                    onMouseEnter={() => {
                        this.setState({ selected: true });
                    }}
                    onMouseLeave={() => {
                        this.setState({ selected: false });
                    }}
                    data-name={this.props.port.name}
                    data-nodeid={this.props.port.getParent().getID()}
                    style={{ color: this.props.model.displayColor }}
                >
                    <i className={this.props.port.linked || this.state.selected ? "icon-caret-right" : "icon-chevron-right"} />
                </div>
            </div>
        );
    }
}