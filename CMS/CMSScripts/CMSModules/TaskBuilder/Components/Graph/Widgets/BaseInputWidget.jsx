class BaseInputWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-input", props);
    }

    render() {
        let valueWidget;

        if (!this.props.port.isLinked()) {
            valueWidget = <BaseInputValueWidget {...this.props} />;
        }

        return (
            <div {...this.getProps()} >
                <BaseParameterIconWidget {...this.props} locked={this.props.port.getModel().inlineOnly} />
                <div className="port-name">
                    {this.props.port.getModel().displayName}
                    {valueWidget}
                </div>
            </div>
        );
    }
}