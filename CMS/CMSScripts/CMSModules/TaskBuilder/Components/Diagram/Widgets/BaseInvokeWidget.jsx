const SRD = window["storm-react-diagrams"];

class BaseInvokeWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-invoke", props);
    }

    getClassName() {
        return super.getClassName();
    }

    render() {
        return (
            <div {...this.getProps()}>
                <BaseCallerIconWidget
                    model={this.props.model}
                />
            </div>
        );
    }
}