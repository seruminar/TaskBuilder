var SRD = window["storm-react-diagrams"];

class BaseNodeFactory extends SRD.AbstractNodeFactory {
    type;

    constructor(type) {
        super(type);

        this.type = type;
    }

    generateReactWidget(diagramEngine, node) {
        return <SRD.DefaultNodeWidget node={node} />;
    }

    getNewInstance() {
        return new BaseNodeModel(type);
    }
}