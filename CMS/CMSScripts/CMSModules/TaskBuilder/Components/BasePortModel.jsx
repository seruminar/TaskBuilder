const SRD = window["storm-react-diagrams"];

class BasePortModel extends SRD.PortModel {
    in: boolean;
    label: string;
    links: { [id: string]: SRD.DefaultLinkModel };
    type: string;

    constructor(isInput: boolean, name: string, portModel = null, id?: string) {
        super(name, "default", id);
        this.in = isInput;
        this.label = portModel.displayName || portModel.name;
        this.type = portModel.type;
    }

    deSerialize(object, engine: SRD.DiagramEngine) {
        super.deSerialize(object, engine);
        this.in = object.in;
        this.label = object.label;
    }

    serialize() {
        return _.merge(super.serialize(), {
            in: this.in,
            label: this.label
        });
    }

    link(port: SRD.PortModel): SRD.LinkModel {
        let link = this.createLinkModel();
        link.setSourcePort(this);
        link.setTargetPort(port);
        return link;
    }

    canLinkToPort(port: SRD.PortModel): boolean {
        if (port instanceof BasePortModel) {
            return this.in !== port.in;
        }
        return true;
    }

    createLinkModel(): SRD.LinkModel {
        let link = super.createLinkModel();
        return link || new SRD.DefaultLinkModel();
    }
}