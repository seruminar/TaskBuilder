﻿const SRD = window["storm-react-diagrams"];

class BaseFunctionFactory extends SRD.AbstractNodeFactory {
    functions = null;

    constructor(functions) {
        super("function");

        this.functions = functions;
    }

    generateReactWidget(engine, node) {
        return <BaseFunctionWidget node={node} diagramEngine={engine} />;
    }

    getNewInstance(initialConfig, signature, forcePorts, locationPoint) {
        const model = _.find(this.functions, f => f.typeIdentifier === signature);

        const nodeModel = new BaseFunctionModel(model, forcePorts);

        if (locationPoint) {
            nodeModel.x = locationPoint.x;
            nodeModel.y = locationPoint.y;
        }

        return nodeModel;
    }
}