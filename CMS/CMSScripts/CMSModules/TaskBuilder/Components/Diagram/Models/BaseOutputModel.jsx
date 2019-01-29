const SRD = window["storm-react-diagrams"];

class BaseOutputModel extends SRD.PortModel {
    constructor(name) {
        super(name, "output");
    }

    getModel() {
        return this.getParent().getFunction().outputs.find(o => o.name === this.getName());
    }

    isLinked = () => _.size(this.links) !== 0;

    canLinkToPort(other) {
        return other instanceof BaseInputModel
            && this.getModel().typeNames.indexOf(other.getModel().typeNames[0]) > -1;
    }

    createLinkModel() {
        return new BaseParameterLinkModel("parameter", this.getModel().displayColor);
    }
}