const SRD = window["storm-react-diagrams"];

class BaseNodeFactory extends SRD.AbstractNodeFactory {
    functionModel;

    constructor(functionModel) {
        super(functionModel.name);

        this.functionModel = functionModel;
    }

    generateReactWidget(diagramEngine, node) {
        return <BaseNodeWidget node={node} diagramEngine={diagramEngine} />;
    }

    getNewInstance(node, forcePorts) {
        let nodeModel = new BaseNodeModel(this.functionModel);

        if (forcePorts) {
            if (this.functionModel.enter !== null) {
                const label = this.functionModel.enterDisplayName || this.functionModel.enter;
                nodeModel.addInPort(label);
            }

            if (this.functionModel.inputs.length) {
                this.functionModel.inputs.map((p, i) => {
                    const label = this.functionModel.inputsDisplayNames[i] || p;
                    nodeModel.addInPort(label);
                });
            }

            if (this.functionModel.leave !== null) {
                const label = this.functionModel.leaveDisplayName || this.functionModel.leave;
                nodeModel.addOutPort(label);
            }

            if (this.functionModel.outputs.length) {
                this.functionModel.outputs.map((p, i) => {
                    const label = this.functionModel.outputsDisplayNames[i] || p;
                    nodeModel.addOutPort(label);
                });
            }
        }

        return nodeModel;
    }
}