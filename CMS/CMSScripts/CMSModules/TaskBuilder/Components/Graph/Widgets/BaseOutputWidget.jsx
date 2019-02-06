class BaseOutputWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-output", props);
    }

    render() {
        return (
            <div {...this.getProps()}>
                <div className="port-name">{this.props.port.getModel().displayName}</div>
                <BaseParameterIconWidget {...this.props} />
            </div>
        );
    }
}