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
                data-name={this.props.model.type}
                data-nodeid={this.props.model.getParent().getID()}
            >
                <i className={this.props.model.linked || this.state.selected ? "icon-caret-right" : "icon-chevron-right"} />
            </div>
        );
    }
}