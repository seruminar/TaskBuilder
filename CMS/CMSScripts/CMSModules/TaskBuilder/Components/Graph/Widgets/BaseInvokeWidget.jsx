class BaseInvokeWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-invoke", props);
    }

    render() {
        return (
            <div {...this.getProps()}>
                <BaseCallerIconWidget {...this.props} />
            </div>
        );
    }
}