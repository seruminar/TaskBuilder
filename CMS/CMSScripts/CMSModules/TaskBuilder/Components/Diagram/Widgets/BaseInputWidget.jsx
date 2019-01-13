const SRD = window["storm-react-diagrams"];

class BaseInputWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-input", props);
    }

    getClassName() {
        return super.getClassName();
    }

    render() {
        return (
            <div {...this.getProps()}>
                <BaseParameterIconWidget
                     model={this.props.model}
                />
                <div className="port-name">
                    {this.props.model.model.displayName}
                    <BaseInputValueWidget {...this.props.model.model} model={this.props.model} />
                </div>
            </div>
        );
    }
}