const SRD = window["storm-react-diagrams"];

class BaseNodeFactory extends SRD.AbstractNodeFactory {
    functionModel;

    constructor(functionModel) {
        super(functionModel.type);

        this.functionModel = functionModel;
    }

    generateReactWidget(diagramEngine: SRD.DiagramEngine, node: BaseNodeModel) {
        return <BaseNodeWidget node={node} diagramEngine={diagramEngine} />;
    }

    getNewInstance(initialConfig?: any, forcePorts): BaseNodeModel {
        return new BaseNodeModel(this.functionModel, forcePorts);
    }
}