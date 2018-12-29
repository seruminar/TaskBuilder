const SRD = window["storm-react-diagrams"];

class BaseNodeWidget extends SRD.BaseWidget {
    constructor(props) {
        super("srd-default-node", props);
        this.state = {};
    }

    generatePort(port) {
        return <SRD.DefaultPortLabel model={port} key={port.id} />;
    }

    render() {
        return (
            <div {...this.getProps()} style={{ background: this.props.node.color }}>
                <div className={this.bem("__title")}>
                    <div className={this.bem("__name")}>{this.props.node.displayName}</div>
                </div>
                <div className={this.bem("__ports")}>
                    <div className={this.bem("__in")}>
                        {_.map(this.props.node.getInPorts(), this.generatePort.bind(this))}
                    </div>
                    <div className={this.bem("__out")}>
                        {_.map(this.props.node.getOutPorts(), this.generatePort.bind(this))}
                    </div>
                </div>
            </div>
        );
    }
}