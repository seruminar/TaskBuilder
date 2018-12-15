var SRD = window["storm-react-diagrams"];

class BaseNodeModel extends SRD.NodeModel {
    name;

    constructor(name) {
        super(name);
        this.name = name;
    }

    addInPort(label) {
        return this.addPort(new SRD.DefaultPortModel(true, SRD.Toolkit.UID(), label));
    }

    addOutPort(label) {
        return this.addPort(new SRD.DefaultPortModel(false, SRD.Toolkit.UID(), label));
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