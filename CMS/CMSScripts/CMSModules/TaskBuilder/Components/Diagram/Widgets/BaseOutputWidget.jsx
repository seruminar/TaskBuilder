const SRD = window["storm-react-diagrams"];

class BaseOutputWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-output", props);
    }

    render() {
        return (
            <div {...this.getProps()}>
                <div className="port-name">{this.props.model.displayName}</div>
                <BaseParameterIconWidget {...this.props} />
            </div>
        );
    }
}