const SRD = window["storm-react-diagrams"];

class BaseOutputModel extends SRD.PortModel {
    constructor(name) {
        super(name, "Output");
    }

    getModel() {
        return this.getParent().getFunction().outputs.find(o => o.name === this.getName());
    }

    isLinked = () => _.size(this.links) !== 0;

    canLinkToPort(other) {
        return other instanceof BaseInputModel
            && other.getModel().typeName === this.getModel().typeName;
    }

    createLinkModel() {
        return new BaseParameterLinkModel("Parameter", this.getModel().displayColor);
    }
}