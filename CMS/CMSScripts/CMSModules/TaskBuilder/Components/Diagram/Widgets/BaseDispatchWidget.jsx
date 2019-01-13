const SRD = window["storm-react-diagrams"];

class BaseDispatchWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-dispatch", props);
    }

    getClassName() {
        return super.getClassName();
    }

    render() {
        if (this.props.model) {
            return (
                <div {...this.getProps()}>
                    <BaseCallerIconWidget
                        model={this.props.model}
                    />
                </div>
            );
        }
        return <div />;
    }
}