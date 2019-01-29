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

    getIcon() {
        if (this.state.selected) {
            return "icon-chevron-right-circle";
        }

        if (this.props.port.isLinked()) {
            return "icon-caret-right";
        }

        return "icon-chevron-right";
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
                <i className={this.getIcon()} />
            </div>
        );
    }
}