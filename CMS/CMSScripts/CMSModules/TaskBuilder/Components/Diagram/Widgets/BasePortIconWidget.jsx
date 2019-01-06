const SRD = window["storm-react-diagrams"];

class BasePortIconWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-icon", props);
        this.state = {
            selected: false
        };
    }

    getClassName() {
        return "port " + super.getClassName() + (this.state.selected ? this.bem("--selected") : "");
    }

    convertDisplayType(displayType: string): string {
        switch (displayType) {
            case "Action":
            case "Void":
                return ">";
            default:
                return displayType;
        }
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
                data-name={this.props.name}
                data-nodeid={this.props.node.getID()}
            >
                {this.convertDisplayType(this.props.type)}
            </div>
        );
    }
}