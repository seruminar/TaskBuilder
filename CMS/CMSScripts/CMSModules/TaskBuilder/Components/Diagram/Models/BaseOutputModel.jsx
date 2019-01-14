const SRD = window["storm-react-diagrams"];

class BaseOutputModel extends SRD.PortModel {
    model = null;
    linked = false;

    constructor(model) {
        if (model) {
            super(model.name, "output");
            this.model = model;
        } else {
            super("", "output");
        }
    }

    deSerialize(other, engine) {
        super.deSerialize(other, engine);
        this.model = other.model;
        this.linked = other.linked;
    }

    serialize() {
        return _.merge(super.serialize(), {
            model: this.model,
            linked: this.linked
        });
    }

    canLinkToPort(other) {
        return other instanceof BaseInputModel
            && other.model.typeName === this.model.typeName;
    }

    createLinkModel() {
        this.linked = true;
        return new BaseParameterLinkModel(this.model.displayColor);
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