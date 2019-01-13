const SRD = window["storm-react-diagrams"];

class BaseFunctionFactory extends SRD.AbstractNodeFactory {
    model;

    constructor(model) {
        super(model.name);

        this.model = model;
    }

    generateReactWidget(diagramEngine: SRD.DiagramEngine, node: BaseFunctionModel) {
        return <BaseFunctionWidget function={node} diagramEngine={diagramEngine} />;
    }

    getNewInstance(initialConfig?: any, forcePorts, locationPoint): BaseFunctionModel {
        const nodeModel = new BaseFunctionModel(this.model, forcePorts);

        if (locationPoint) {
            nodeModel.x = locationPoint.x;
            nodeModel.y = locationPoint.y;
        }

        return nodeModel;
    }
}