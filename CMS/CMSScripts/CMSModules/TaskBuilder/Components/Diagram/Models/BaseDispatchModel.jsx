const SRD = window["storm-react-diagrams"];

class BaseDispatchModel extends SRD.PortModel {
    linked = false;

    constructor(name) {
        if (name) {
            super(name, "dispatch");
        } else {
            super("", "dispatch");
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

    canLinkToPort(other) {
        return other instanceof BaseInvokeModel;
    }

    createLinkModel() {
        return new BaseCallerLinkModel(this.getParent().function.displayColor);
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