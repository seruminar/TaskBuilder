const SRD = window["storm-react-diagrams"];

class BaseLinkFactory extends SRD.AbstractLinkFactory {
    generateReactWidget(diagramEngine, link) {
        return <BaseLinkWidget link={link} diagramEngine={diagramEngine} />;
    }

    getNewInstance(initialConfig) {
        switch (this.type) {
            case "parameter":
                return new BaseParameterLinkModel(this.type);
            default:
                return new BaseCallerLinkModel(this.type);
        }
    }

    generateLinkSegment(model, widget, selected, path) {
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