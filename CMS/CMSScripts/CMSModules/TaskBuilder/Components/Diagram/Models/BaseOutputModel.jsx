const SRD = window["storm-react-diagrams"];

class BaseOutputModel extends SRD.PortModel {
    linked = false;

    constructor(name) {
        if (name) {
            super(name, "output");
        } else {
            super("", "output");
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
        return this.getParent().function.outputs.find(o => o.name === this.getName());
    }

    canLinkToPort(other) {
        return other instanceof BaseInputModel
            && other.getModel().typeName === this.getModel().typeName;
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