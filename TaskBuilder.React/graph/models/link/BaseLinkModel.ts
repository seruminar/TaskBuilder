import * as _ from "lodash";
import { LinkModel, LabelModel, DefaultLabelModel, DefaultLinkModelListener } from "storm-react-diagrams";

import { GraphEngine } from "../../GraphEngine";

export class BaseLinkModel extends LinkModel<DefaultLinkModelListener> {
    width: number;
    curvyness: number;
    color: string;

    deSerialize(other: any, engine: GraphEngine) {
        super.deSerialize(other, engine);
        this.color = other.color;
    }

    serialize() {
        return _.merge(super.serialize(), {
            color: this.color
        });
    }

    addLabel(label: LabelModel) {
        if (label instanceof LabelModel) {
            return super.addLabel(label);
        }

        const labelOb = new DefaultLabelModel();
        labelOb.setLabel(label);

        return super.addLabel(labelOb);
    }

    setWidth(width: number) {
        this.width = width;

        this.iterateListeners((listener, event) => {
            if (listener.widthChanged) {
                listener.widthChanged({ ...event, width: width });
            }
        });
    }

    setColor(color: string) {
        this.color = color;

        this.iterateListeners((listener, event) => {
            if (listener.colorChanged) {
                listener.colorChanged({ ...event, color: color });
            }
        });
    }
}