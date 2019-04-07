import * as React from "react";

import { AbstractLinkFactory } from "storm-react-diagrams";
import { ParameterLinkModel } from "../models/link/ParameterLinkModel";
import { CallerLinkModel } from "../models/link/CallerLinkModel";
import { GraphEngine } from "../GraphEngine";
import { BaseLinkModel } from "../models/link/BaseLinkModel";
import { BaseLinkWidget } from "../widgets/link/BaseLinkWidget";

export class LinkFactory extends AbstractLinkFactory {
    generateReactWidget(diagramEngine: GraphEngine, link: BaseLinkModel) {
        return <BaseLinkWidget link={link} diagramEngine={diagramEngine} />;
    }

    getNewInstance() {
        switch (this.type) {
            case "parameter":
                return new ParameterLinkModel(this.type);
            default:
                return new CallerLinkModel(this.type);
        }
    }

    generateLinkSegment(model: BaseLinkModel, widget: BaseLinkWidget, selected: boolean, path: string) {
        return (
            <path
                className={selected ? widget.bem("-path-selected") : ""}
                strokeWidth={model.width}
                stroke={model.color}
                d={path}
            />
        );
    }
}