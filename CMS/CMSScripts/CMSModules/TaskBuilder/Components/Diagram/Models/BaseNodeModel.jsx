const SRD = window["storm-react-diagrams"];

class BaseNodeModel extends SRD.NodeModel {
    name: string;
    displayName: string;

    constructor(functionModel) {
        super(functionModel.name);
        this.name = functionModel.name;
        this.displayName = functionModel.displayName || functionModel.name;
    }

    addInPort(portModel) {
        return this.addPort(new BasePortModel(portModel));
    }

    addOutPort(portModel) {
        return this.addPort(new BasePortModel(portModel));
    }

    getInPorts() {
        return _.filter(this.ports, portModel => {
            switch (portModel.type) {
                case "enter":
                case "input":
                    return true;
            }
        });
    }

    getOutPorts() {
        return _.filter(this.ports, portModel => {
            switch (portModel.type) {
                case "leave":
                case "output":
                    return true;
            }
        });
    }
}