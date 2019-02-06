class BaseDispatchWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-dispatch", props);
    }

    render() {
        return (
            <div {...this.getProps()}>
                <div className="port-name">{this.props.port.getModel().displayName}</div>
                <BaseCallerIconWidget {...this.props} />
            </div>
        );
    }
}