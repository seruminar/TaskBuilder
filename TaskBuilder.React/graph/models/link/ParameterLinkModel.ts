import * as _ from "lodash";

import { OutputModel } from "../port/OutputModel";
import { BasePortModel } from "../port/BasePortModel";
import { BaseLinkModel } from "./BaseLinkModel";

export class ParameterLinkModel extends BaseLinkModel {
    width = 2;
    curvyness = 50;
    color = "";

    constructor(type: string, linkColor: string = "") {
        super(type);
        this.color = linkColor;
    }

    setTargetPort(port: BasePortModel) {
        // The order and inheritance of these calls is important.
        // This allows for links to be created in the reverse direction.
        if (port instanceof OutputModel) {
            if (this.getTargetPort() === null) {
                super.setTargetPort(this.getSourcePort());
                this.setSourcePort(null as any);
                this.getTargetPort().addLink(this);
            }
            this.setSourcePort(port);
        } else {
            super.setTargetPort(port);
        }
    }
}