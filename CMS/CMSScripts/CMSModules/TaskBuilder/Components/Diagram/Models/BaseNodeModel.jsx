const SRD = window["storm-react-diagrams"];

class BaseNodeModel extends SRD.NodeModel {
    displayName: string;

    constructor(functionModel, forcePorts) {
        super(functionModel.type);
        this.displayName = functionModel.displayName;

        if (forcePorts) {
            if (functionModel.enter !== null) {
                this.addPort(functionModel.enter);
            }

            if (functionModel.inputs.length) {
                functionModel.inputs.map((p) => {
                    this.addPort(p);
                });
            }

            if (functionModel.leave !== null) {
                this.addPort(functionModel.leave);
            }

            if (functionModel.outputs.length) {
                functionModel.outputs.map((p) => {
                    this.addPort(p);
                });
            }
        }
        console.log(this.ports);
    }

    deSerialize(other, engine: SRD.DiagramEngine) {
        super.deSerialize(other, engine);
        this.displayName = other.displayName;
    }

    serialize() {
        // Hack to revert version of _ that was loaded by Kentico's cmsrequire
        if (_.VERSION === "1.5.2") _.noConflict();

        return _.merge(super.serialize(), {
            displayName: this.displayName
        });
    }

    addPort(portModel) {
        return super.addPort(new BasePortModel(portModel));
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