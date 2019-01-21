const SRD = window["storm-react-diagrams"];

class BaseDispatchWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-dispatch", props);
    }

    render() {
        return (
            <div {...this.getProps()}>
                <BaseCallerIconWidget {...this.props} />
            </div>
        );
    }
}