﻿const SRD = window["storm-react-diagrams"];

class BaseInputWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-input", props);
    }

    render() {
        let valueWidget;

        if (!(this.props.port.linked)) {
            valueWidget = <BaseInputValueWidget {...this.props} />;
        }

        // Partial hack to redraw links when value widget disappears
        //else if (window.diagram.diagramEngine.canvas) {
        //    _.forEach(this.props.model.links, link => {
        //        link.points[1].updateLocation(window.diagram.diagramEngine.getPortCenter(link.targetPort));
        //    });
        //}

        return (
            <div {...this.getProps()} >
                <BaseParameterIconWidget {...this.props} />
                <div className="port-name">
                    {this.props.model.displayName}
                    {valueWidget}
                </div>
            </div>
        );
    }
}