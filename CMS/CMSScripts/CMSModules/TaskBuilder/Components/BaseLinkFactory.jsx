﻿var SRD = window["storm-react-diagrams"];

class BaseLinkFactory extends SRD.AbstractLinkFactory {
    constructor(type) {
        super(type);
    }

    generateReactWidget(diagramEngine, link) {
        return <SRD.DefaultLinkWidget link={link} diagramEngine={diagramEngine} />;
    }

    getNewInstance(initialConfig) {
        return new SRD.DefaultLinkModel();
    }

    generateLinkSegment(model, widget, selected, path) {
        return (
            <path
                className={selected ? widget.bem("--path-selected") : ""}
                strokeWidth={model.width}
                stroke={model.color}
                d={path}
            />
        );
    }
}