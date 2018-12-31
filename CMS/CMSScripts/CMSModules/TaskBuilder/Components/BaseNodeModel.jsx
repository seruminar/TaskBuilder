const SRD = window["storm-react-diagrams"];

class BaseNodeModel extends SRD.NodeModel {
    name;
    displayName;

    constructor(functionModel) {
        super(functionModel.name);
        this.name = functionModel.name;
        this.displayName = functionModel.displayName || functionModel.name;
    }

    addInPort(portModel) {
        return this.addPort(new BasePortModel(true, SRD.Toolkit.UID(), portModel));
    }

    addOutPort(portModel) {
        return this.addPort(new BasePortModel(false, SRD.Toolkit.UID(), portModel));
    }

    getInPorts() {
        return _.filter(this.ports, portModel => {
            return portModel.in;
        });
    }

    getOutPorts() {
        return _.filter(this.ports, portModel => {
            return !portModel.in;
        });
    }
}