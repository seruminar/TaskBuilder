class BaseParameterIconWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port", props);
    }

    state = {
        selected: false
    };

    getClassName() {
        return super.getClassName() + (this.state.selected ? this.bem("-selected") : "");
    }

    select = (select) => {
        if (!this.props.locked) {
            this.setState({ selected: select });
        }
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
            <div className="port-icon">
                <div
                    {...this.getProps()}
                    onMouseEnter={() => this.select(true)}
                    onMouseLeave={() => this.select(false)}
                    data-name={this.props.port.name}
                    data-nodeid={this.props.port.getParent().getID()}
                    style={{ color: this.props.port.getModel().displayColor }}
                >
                    <i
                        className={this.getIcon()}
                        style={{ opacity: this.props.locked ? 0 : 1 }}
                    />
                </div>
            </div>
        );
    }
}