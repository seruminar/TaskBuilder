const SRD = window["storm-react-diagrams"];

class BaseOutputWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-output", props);
    }

    getClassName() {
        return super.getClassName();
    }

    render() {
        return (
            <div {...this.getProps()}>
                <div className="port-name">{this.props.model.model.displayName}</div>
                <BaseParameterIconWidget
                    model={this.props.model}
                />
            </div>
        );
    }
}