const SRD = window["storm-react-diagrams"];

class BaseInputModel extends SRD.PortModel {
    linked = false;

    constructor(name) {
        if (name) {
            super(name, "input");
        } else {
            super("", "input");
        }
    }

    deSerialize(other, engine) {
        super.deSerialize(other, engine);
        this.linked = other.linked;
    }

    serialize() {
        return _.merge(super.serialize(), {
            linked: this.linked
        });
    }

    getModel() {
        return this.getParent().function.inputs.find(o => o.name === this.getName());
    }

    canLinkToPort(other) {
        //return other instanceof BaseOutputModel
        //    && other.model.typeName === this.model.typeName;
        return false;
    }

    createLinkModel() {
        return new BaseParameterLinkModel(this.getModel().displayColor);
    }

    addLink(link) {
        super.addLink(link);
        this.linked = true;
    }

    removeLink(link) {
        super.removeLink(link);
        if (_.size(this.links) === 0) {
            this.linked = false;
        }
    }
}