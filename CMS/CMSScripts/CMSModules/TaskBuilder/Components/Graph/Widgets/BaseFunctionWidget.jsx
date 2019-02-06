class BaseFunctionWidget extends SRD.BaseWidget {
    constructor(props) {
        super("function", props);
    }

    render() {
        const { displayName, displayColor } = this.props.node.getFunction();

        return (
            <div {...this.getProps()} >
                <div className="function-shadow">
                    <div
                        className={this.bem("-title")}
                    >
                        <div
                            className={this.bem("-name")}
                            style={{ boxShadow: "inset 0 0.9em 3em " + displayColor }}
                        >
                            {displayName}
                        </div>
                    </div>
                    <div className={this.bem("-ports")}>
                        <div className={this.bem("-in")}>
                            <BaseInvokeWidget port={this.props.node.getInvoke()} />
                            {this.props.node.getInputs().map(p =>
                                <BaseInputWidget port={p} key={p.getID()} />
                            )}
                        </div>
                        <div className={this.bem("-out")}>
                            {this.props.node.getDispatchs().map(p =>
                                <BaseDispatchWidget port={p} key={p.getID()} />
                            )}
                            {this.props.node.getOutputs().map(p =>
                                <BaseOutputWidget port={p} key={p.getID()} />
                            )}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}