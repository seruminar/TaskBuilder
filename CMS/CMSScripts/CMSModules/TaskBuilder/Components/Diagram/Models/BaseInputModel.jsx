const SRD = window["storm-react-diagrams"];

class BaseInputModel extends SRD.PortModel {
    constructor(name) {
        super(name, "Input");
    }

    getModel() {
        return this.getParent().getFunction().inputs.find(o => o.name === this.getName());
    }

    linked = _.size(this.links) !== 0;

    canLinkToPort(other) {
        //return other instanceof BaseOutputModel
        //    && other.model.typeName === this.model.typeName;
        return false;
    }

    createLinkModel() {
        return new BaseParameterLinkModel(this.getModel().displayColor);
    }
}