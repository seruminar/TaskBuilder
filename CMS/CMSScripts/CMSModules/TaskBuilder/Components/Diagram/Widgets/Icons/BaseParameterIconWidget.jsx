const SRD = window["storm-react-diagrams"];

class BaseParameterIconWidget extends SRD.BaseWidget {
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
                data-name={this.props.model.model.name}
                data-nodeid={this.props.model.getParent().getID()}
                style={{ color: this.props.model.model.displayColor}}
            >
                <i className={this.props.model.linked || this.state.selected ? "icon-caret-right" : "icon-chevron-right"}/>
            </div>
        );
    }
}