const SRD = window["storm-react-diagrams"];

class BaseFunctionFactory extends SRD.AbstractNodeFactory {
    functions = null;

    constructor(functions) {
        super("function");

        this.functions = functions;
    }

    generateReactWidget(engine, node) {
        return <BaseFunctionWidget node={node} diagramEngine={engine} />;
    }

    getNewInstance(initialConfig, typeGuid, forcePorts, locationPoint) {
        const func = _.find(this.functions, f => f.typeGuid === typeGuid);

        let funcClone;

        if (func) {
            funcClone = JSON.parse(JSON.stringify(func));
        }

        const nodeModel = new BaseFunctionModel(funcClone, forcePorts);

        if (locationPoint) {
            nodeModel.x = locationPoint.x;
            nodeModel.y = locationPoint.y;
        }

        return nodeModel;
    }
}