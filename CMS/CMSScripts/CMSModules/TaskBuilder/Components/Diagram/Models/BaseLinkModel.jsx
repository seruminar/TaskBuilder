const SRD = window["storm-react-diagrams"];

class BaseLinkModel extends SRD.LinkModel {
    width: number;
    curvyness: number;
    color: string;

    constructor(displayColor: string) {
        super("default");
        this.width = 3;
        this.curvyness = 50;
        this.color = displayColor;
    }

    serialize() {
        // Hack to revert version of _ that was loaded by Kentico's cmsrequire
        if (_.VERSION === "1.5.2") _.noConflict();

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

    setTargetPort(port: BasePortModel) {
        if (port && this.sourcePort) {
            if (this.sourcePort.type === "leave" && port.type === "enter") {
                this.type = "caller";
            }
            else if (this.sourcePort.type === "output" && port.type === "input") {
                this.type = "property";
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