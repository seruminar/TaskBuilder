const SRD = window["storm-react-diagrams"];

class BaseFunctionWidget extends SRD.BaseWidget {
    constructor(props) {
        super("function", props);
    }

    render() {
        return (
            <div {...this.getProps()} >
                <div className="function-shadow">
                    <div
                        className={this.bem("-title")}
                    >
                        <div
                            className={this.bem("-name")}
                            style={{ boxShadow: "inset 0 0.9em 3em " + this.props.node.function.displayColor }}
                        >
                            {this.props.node.function.displayName}
                        </div>
                    </div>
                    <div className={this.bem("-ports")}>
                        <div className={this.bem("-in")}>
                            <BaseInvokeWidget port={this.props.node.getInvoke()} />
                            {this.props.node.getInputs().map(p =>
                                <BaseInputWidget port={p} model={p.getModel()} key={p.id} />
                            )}
                        </div>
                        <div className={this.bem("-out")}>
                            {this.props.node.getDispatchs().map(p =>
                                <BaseDispatchWidget port={p} key={p.id} />
                            )}
                            {this.props.node.getOutputs().map(p =>
                                <BaseOutputWidget port={p} model={p.getModel()} key={p.id} />
                            )}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}