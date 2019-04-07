import * as _ from "lodash";

import { DispatchModel } from "../port/DispatchModel";
import { BasePortModel } from "../port/BasePortModel";
import { BaseLinkModel } from "./BaseLinkModel";

export class CallerLinkModel extends BaseLinkModel {
    width = 3;
    curvyness = 75;
    color = "";

    constructor(type: string, linkColor: string = "") {
        super(type);
        this.color = linkColor;
    }

    setTargetPort(port: BasePortModel) {
        // The order and inheritance of these calls is important.
        // This allows for links to be created in the reverse direction.
        if (port instanceof DispatchModel) {
            if (this.getTargetPort() === null) {
                super.setTargetPort(this.getSourcePort());
                this.setSourcePort(null as any);
                this.getTargetPort().addLink(this);
            }
            this.setSourcePort(port);
            this.setColor((this.getSourcePort() as BasePortModel).parentModel.getFunction().displayColor);
        } else {
            super.setTargetPort(port);
        }
    }
}