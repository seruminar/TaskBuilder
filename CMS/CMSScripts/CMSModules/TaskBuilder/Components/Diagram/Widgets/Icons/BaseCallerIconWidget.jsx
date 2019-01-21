const SRD = window["storm-react-diagrams"];

class BaseCallerIconWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-icon", props);
        this.state = {
            selected: false
        };
    }

    getClassName() {
        return "port " + super.getClassName() + (this.state.selected ? this.bem("-selected") : "");
    }

    render() {
        return (
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
            >
                <i className={this.props.port.linked || this.state.selected ? "icon-caret-right" : "icon-chevron-right"} />
            </div>
        );
    }
}