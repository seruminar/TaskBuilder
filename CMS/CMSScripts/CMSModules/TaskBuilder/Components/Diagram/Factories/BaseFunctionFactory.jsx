const SRD = window["storm-react-diagrams"];

class BaseFunctionFactory extends SRD.AbstractNodeFactory {
    model = null;

    constructor(model) {
        super(model.name);

        this.model = model;
    }

    generateReactWidget(engine, node) {
        return <BaseFunctionWidget function={node} diagramEngine={engine} />;
    }

    getNewInstance(initialConfig, forcePorts, locationPoint) {
        const nodeModel = new BaseFunctionModel(this.model, forcePorts);

        if (locationPoint) {
            nodeModel.x = locationPoint.x;
            nodeModel.y = locationPoint.y;
        }

        return nodeModel;
    }
}