const SRD = window["storm-react-diagrams"];

class BaseInvokeModel extends SRD.PortModel {
    model;
    linked;
    linkColor;

    constructor(model, linkColor) {
        if (model) {
            super(model.name, "invoke");
            this.model = model;
        } else {
            super("", "invoke");
        }

        this.linkColor = linkColor;
    }

    deSerialize(other, engine: SRD.DiagramEngine) {
        super.deSerialize(other, engine);
        this.model = other.model;
        this.linked = other.linked;
        this.linkColor = other.linkColor;
    }

    serialize() {
        return _.merge(super.serialize(), {
            model: this.model,
            linked: this.linked,
            linkColor: this.linkColor
        });
    }

    canLinkToPort(other): boolean {
        //return other instanceof BaseDispatchModel;
        return false;
    }

    createLinkModel(): SRD.LinkModel {
        return new BaseCallerLinkModel(this.linkColor);
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