var SRD = window["storm-react-diagrams"];

class BaseNodeFactory extends SRD.AbstractNodeFactory {
    functionModel;

    constructor(functionModel) {
        super(functionModel.Name);

        this.functionModel = functionModel;
    }

    generateReactWidget(diagramEngine, node) {
        return <SRD.DefaultNodeWidget node={node} diagramEngine={diagramEngine}/>;
    }

    getNewInstance() {
        let nodeModel = new BaseNodeModel(this.functionModel.Name);

        //if (this.functionModel.Enter !== null) {
        //    nodeModel.addInPort(this.functionModel.Enter);
        //}

        //if (this.functionModel.Inputs.length) {
        //    this.functionModel.Inputs.map((p) => nodeModel.addInPort(p));
        //}

        //if (this.functionModel.Leave !== null) {
        //    nodeModel.addOutPort(this.functionModel.Leave);
        //}

        //if (this.functionModel.Outputs.length) {
        //    this.functionModel.Outputs.map((p) => nodeModel.addOutPort(p));
        //}

        return nodeModel;
    }
}