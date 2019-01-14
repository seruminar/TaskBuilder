const SRD = window["storm-react-diagrams"];

class BaseInputWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-input", props);
    }

    getClassName() {
        return super.getClassName();
    }

    render() {
        let valueWidget;

        if (!(this.props.model.linked)) {
            valueWidget = <BaseInputValueWidget {...this.props.model.model} model={this.props.model} />;
        }
        else {
            // Partial hack to redraw links when value widget disappears
            _.forEach(this.props.model.links, link => {
                link.points[1].updateLocation(window.diagram.diagramEngine.getPortCenter(link.targetPort));
            });
        }
        return (
            <div
                {...this.getProps()}
            >
                <BaseParameterIconWidget
                    model={this.props.model}
                />
                <div className="port-name">
                    {this.props.model.model.displayName}
                    {valueWidget}
                </div>
            </div>
        );
    }
}