const SRD = window["storm-react-diagrams"];

class BaseInputModel extends SRD.PortModel {
    model;
    linked;
    value;

    constructor(model) {
        if (model) {
            super(model.name, "input");
            this.model = model;
            this.value = model.defaultValue;
        } else {
            super("", "input");
        }
    }

    deSerialize(other, engine: SRD.DiagramEngine) {
        super.deSerialize(other, engine);
        this.model = other.model;
        this.value = other.value;
        this.linked = other.linked;
    }

    serialize() {
        return _.merge(super.serialize(), {
            model: this.model,
            value: this.value,
            linked: this.linked
        });
    }

    setValue(value) {
        this.value = value;
    }

    canLinkToPort(other): boolean {
        //return other instanceof BaseOutputModel
        //    && other.model.typeName === this.model.typeName;
        return false;
    }

    createLinkModel(): SRD.LinkModel {
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