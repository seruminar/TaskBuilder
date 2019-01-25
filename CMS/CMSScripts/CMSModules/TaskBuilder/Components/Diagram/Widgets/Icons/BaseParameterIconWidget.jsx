const SRD = window["storm-react-diagrams"];

class BaseParameterIconWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port", props);
        this.state = {
            selected: false
        };

        if (props.locked) {
            this.props.port.setLocked(true);
        }
    }

    getClassName() {
        return super.getClassName() + (this.state.selected ? this.bem("-selected") : "");
    }

    select = (select) => {
        if (!this.props.locked) {
            this.setState({ selected: select });
        }
    }

    render() {
        return (
            <div className="port-icon">
                <div
                    {...this.getProps()}
                    onMouseEnter={() => this.select(true)}
                    onMouseLeave={() => this.select(false)}
                    data-name={this.props.port.name}
                    data-nodeid={this.props.port.getParent().getID()}
                    style={{ color: this.props.model.displayColor }}
                >
                    <i
                        className={this.props.port.isLinked() || this.state.selected ? "icon-caret-right" : "icon-chevron-right"}
                        style={{ opacity: this.props.locked ? 0 : 1 }}
                    />
                </div>
            </div>
        );
    }
}