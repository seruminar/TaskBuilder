const SRD = window["storm-react-diagrams"];

class BaseLinkFactory extends SRD.AbstractLinkFactory {
    generateReactWidget(diagramEngine, link) {
        return <SRD.DefaultLinkWidget link={link} diagramEngine={diagramEngine} />;
    }

    getNewInstance(initialConfig) {
        switch (this.type) {
            case "Parameter":
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