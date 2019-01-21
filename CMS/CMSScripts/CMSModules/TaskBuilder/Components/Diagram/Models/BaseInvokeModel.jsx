const SRD = window["storm-react-diagrams"];

class BaseInvokeModel extends SRD.PortModel {
    linked = false;

    constructor(name, linkColor) {
        if (name) {
            super(name, "invoke");
        } else {
            super("", "invoke");
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
        //return other instanceof BaseDispatchModel;
        return false;
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