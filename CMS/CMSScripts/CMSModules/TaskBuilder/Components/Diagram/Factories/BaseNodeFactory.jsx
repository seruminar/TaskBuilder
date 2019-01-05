const SRD = window["storm-react-diagrams"];

class BaseNodeFactory extends SRD.AbstractNodeFactory {
    functionModel;

    constructor(functionModel) {
        super(functionModel.name);

        this.functionModel = functionModel;
    }

    generateReactWidget(diagramEngine: SRD.DiagramEngine, node: BaseNodeModel) {
        return <BaseNodeWidget node={node} diagramEngine={diagramEngine} />;
    }

    getNewInstance(node, forcePorts) {
        let nodeModel = new BaseNodeModel(this.functionModel);

        if (forcePorts) {
            if (this.functionModel.enter !== null) {
                nodeModel.addInPort(this.functionModel.enter);
            }

            if (this.functionModel.inputs.length) {
                this.functionModel.inputs.map((p) => {
                    nodeModel.addInPort(p);
                });
            }

            if (this.functionModel.leave !== null) {
                nodeModel.addOutPort(this.functionModel.leave);
            }

            if (this.functionModel.outputs.length) {
                this.functionModel.outputs.map((p) => {
                    nodeModel.addOutPort(p);
                });
            }
        }

        return nodeModel;
    }
}