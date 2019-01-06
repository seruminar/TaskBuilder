const SRD = window["storm-react-diagrams"];

class BasePortModel extends SRD.PortModel {
    displayName: string;
    displayType: string;
    displayColor: string;

    constructor(portModel) {
        if (portModel) {
            super(portModel.displayName, portModel.type, portModel.id);
            this.displayName = portModel.displayName;
            this.displayType = portModel.displayType;
            this.displayColor = portModel.displayColor;
        } else {
            super();
        }
    }

    deSerialize(other, engine: SRD.DiagramEngine) {
        super.deSerialize(other, engine);
        this.displayName = other.displayName;
        this.displayType = other.displayType;
        this.displayColor = other.displayColor;
    }

    serialize() {
        // Hack to revert version of _ that was loaded by Kentico's cmsrequire
        if (_.VERSION === "1.5.2") _.noConflict();

        return _.merge(super.serialize(), {
            displayName: this.displayName,
            displayType: this.displayType,
            displayColor: this.displayColor
        });
    }

    canLinkToPort(other: BasePortModel): boolean {
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

    // This function depends on the max # of links in super, try handling this when arrays are supported
    createLinkModel(): SRD.LinkModel {
        return new BaseLinkModel(this.displayColor);
    }
}