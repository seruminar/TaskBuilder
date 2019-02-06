class BaseFunctionFactory extends SRD.AbstractNodeFactory {
    constructor() {
        super("function");
    }

    generateReactWidget(engine, node) {
        return <BaseFunctionWidget node={node} diagramEngine={engine} />;
    }

    getNewInstance(node, typeGuid, locationPoint) {
        typeGuid = typeGuid || node.functionTypeGuid;

        const func = taskBuilderDataSource.getFunctionByTypeGuid(typeGuid);

        const nodeModel = new BaseFunctionModel(func, locationPoint);
        nodeModel.setParent(diagramEngine.getDiagramModel());

        _.forEach(nodeModel.getInputs(), i => {
            switch (i.getModel().inputType) {
                case "structureOnly":
                case "filled":
                    taskBuilderDataSource.setInputValue(i.getID(), i.getModel().filledModel);
            }
        });

        return nodeModel;
    }
}