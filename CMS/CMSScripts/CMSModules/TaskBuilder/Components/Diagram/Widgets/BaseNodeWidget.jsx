const SRD = window["storm-react-diagrams"];

class BaseNodeWidget extends SRD.BaseWidget {
    constructor(props) {
        super("srd-default-node", props);
    }

    render() {
        return (
            <div {...this.getProps()} style={{ background: this.props.node.color }}>
                <div className={this.bem("__title")}>
                    <div className={this.bem("__name")}>{this.props.node.displayName}</div>
                </div>
                <div className={this.bem("__ports")}>
                    <div className={this.bem("__in")}>
                        {this.props.node.getInPorts().map(function (p) {
                            return <BasePortWidget model={p} key={p.id} />;
                        })}
                    </div>
                    <div className={this.bem("__out")}>
                        {this.props.node.getOutPorts().map(function (p) {
                            return <BasePortWidget model={p} key={p.id} />;
                        })}
                    </div>
                </div>
            </div>
        );
    }
}