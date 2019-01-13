const SRD = window["storm-react-diagrams"];

class BaseCallerLinkModel extends SRD.LinkModel {
    width: number;
    curvyness: number;
    color: string;

    constructor(linkColor: string) {
        super("caller");
        this.width = 4;
        this.curvyness = 50;
        this.color = linkColor;
    }

    serialize() {
        return _.merge(super.serialize(), {
            width: this.width,
            curvyness: this.curvyness,
            color: this.color
        });
    }

    deSerialize(ob, engine: SRD.DiagramEngine) {
        super.deSerialize(ob, engine);
        this.width = ob.width;
        this.curvyness = ob.curvyness;
        this.color = ob.color;
    }

    setTargetPort(port) {
        if (port && this.sourcePort) {
            if (this.sourcePort.type === "dispatch" && port.type === "invoke") {
                this.type = "caller";
            }
        }

        super.setTargetPort(port);
    }

    addLabel(label: SRD.LabelModel | string) {
        if (label instanceof SRD.LabelModel) {
            return super.addLabel(label);
        }

        let labelOb = new SRD.DefaultLabelModel();
        labelOb.setLabel(label);

        return super.addLabel(labelOb);
    }

    setWidth(width: number) {
        this.width = width;

        this.iterateListeners((listener: SRD.DefaultLinkModelListener, event: vBaseEvent) => {
            if (listener.widthChanged) {
                listener.widthChanged({ ...event, width: width });
            }
        });
    }

    setColor(color: string) {
        this.color = color;

        this.iterateListeners((listener: SRD.DefaultLinkModelListener, event: SRD.BaseEvent) => {
            if (listener.colorChanged) {
                listener.colorChanged({ ...event, color: color });
            }
        });
    }
}