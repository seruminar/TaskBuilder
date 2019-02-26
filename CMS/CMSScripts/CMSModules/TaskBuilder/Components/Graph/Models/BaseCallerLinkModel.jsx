class BaseCallerLinkModel extends SRD.LinkModel {
    width = 3;
    curvyness = 75;
    color = null;

    constructor(type, linkColor) {
        super(type);
        this.color = linkColor;
    }

    deSerialize(other, engine) {
        super.deSerialize(other, engine);
        this.color = other.color;
    }

    serialize() {
        return _.merge(super.serialize(), {
            color: this.color
        });
    }

    setTargetPort(port) {
        // The order and inheritance of these calls is important.
        // This allows for links to be created in the reverse direction.
        if (port instanceof BaseDispatchModel) {
            if (this.getTargetPort() === null) {
                super.setTargetPort(this.getSourcePort());
                this.setSourcePort(null);
                this.getTargetPort().addLink(this);
            }
            this.setSourcePort(port);
            this.setColor(this.getSourcePort().getParent().getFunction().displayColor);
        } else {
            super.setTargetPort(port);
        }
    }

    addLabel(label) {
        if (label instanceof SRD.LabelModel) {
            return super.addLabel(label);
        }

        const labelOb = new SRD.DefaultLabelModel();
        labelOb.setLabel(label);

        return super.addLabel(labelOb);
    }

    setWidth(width) {
        this.width = width;

        this.iterateListeners((listener, event) => {
            if (listener.widthChanged) {
                listener.widthChanged({ ...event, width: width });
            }
        });
    }

    setColor(color) {
        this.color = color;

        this.iterateListeners((listener, event) => {
            if (listener.colorChanged) {
                listener.colorChanged({ ...event, color: color });
            }
        });
    }
}