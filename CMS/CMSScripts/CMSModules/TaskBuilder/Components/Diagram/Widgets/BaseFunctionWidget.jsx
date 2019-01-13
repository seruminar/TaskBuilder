const SRD = window["storm-react-diagrams"];

class BaseFunctionWidget extends SRD.BaseWidget {
    constructor(props) {
        super("function", props);
    }

    render() {
        return (
            <div {...this.getProps()} >
                <div
                    className={this.bem("-title")}
                >
                    <div
                        className={this.bem("-name")}
                        style={{ boxShadow: "inset 0 0.9em 3em " + this.props.function.model.displayColor }}
                    >
                        {this.props.function.model.displayName}
                    </div>
                </div>
                <div className={this.bem("-ports")}>
                    <div className={this.bem("-in")}>
                        <BaseInvokeWidget model={this.props.function.getInvoke()} />
                        {this.props.function.getInputs().map(p =>
                            <BaseInputWidget model={p} key={p.id} />
                        )}
                    </div>
                    <div className={this.bem("-out")}>
                        <BaseDispatchWidget model={this.props.function.getDispatch()} />
                        {this.props.function.getOutputs().map(p =>
                            <BaseOutputWidget model={p} key={p.id} />
                        )}
                    </div>
                </div>
            </div>
        );
    }
}