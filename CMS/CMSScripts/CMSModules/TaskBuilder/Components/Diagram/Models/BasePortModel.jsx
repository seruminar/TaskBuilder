const SRD = window["storm-react-diagrams"];

class BasePortModel extends SRD.PortModel {
    displayName: string;
    displayType: string;
    links: { [id: string]: SRD.DefaultLinkModel };

    constructor(portModel) {
        super(portModel.name, portModel.type, null);
        this.displayName = portModel.displayName || portModel.name;
        this.displayType = portModel.displayType;
    }

    deSerialize(other, engine: SRD.DiagramEngine) {
        super.deSerialize(other, engine);
        this.displayName = other.displayName;
        this.displayType = other.displayType;
    }

    serialize() {
        // Hack to revert version of _ that was loaded by Kentico's cmsrequire
        if (_.VERSION === "1.5.2") _.noConflict();

        return _.merge(super.serialize(), {
            portType: this.portType,
            displayName: this.displayName,
            displayType: this.displayType
        });
    }

    link(port: BasePortModel): SRD.LinkModel {
        let link = this.createLinkModel();

        link.setSourcePort(this);
        link.setTargetPort(port);

        return link;
    }

    canLinkToPort(other: BasePortModel): boolean {
        console.log(this);
        console.log(other);

        if (other instanceof BasePortModel) {
            if (this.type === "leave" && other.type === "enter") {
                return true;
            }
            else if (this.type === "output" && other.type === "input") {
                return this.displayType === other.displayType;
            }
        }
        return false;
    }

    createLinkModel(): SRD.LinkModel {
        let link = super.createLinkModel();
        return link || new SRD.DefaultLinkModel();
    }
}