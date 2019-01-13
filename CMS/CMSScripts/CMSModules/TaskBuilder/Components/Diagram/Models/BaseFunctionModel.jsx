const SRD = window["storm-react-diagrams"];

class BaseFunctionModel extends SRD.NodeModel {
    model;

    constructor(model, forcePorts) {
        super(model.name);
        this.model = model;

        if (forcePorts) {
            this.addInvoke(model.invoke, model.displayColor);

            if (model.dispatch !== null) {
                this.addDispatch(model.dispatch, model.displayColor);
            }

            if (model.inputs.length) {
                model.inputs.map(i => this.addInput(i));
            }

            if (model.outputs.length) {
                model.outputs.map(o => this.addOutput(o));
            }
        }
    }

    addInvoke(model, linkColor) {
        return super.addPort(new BaseInvokeModel(model, linkColor));
    }

    addDispatch(model, linkColor) {
        return super.addPort(new BaseDispatchModel(model, linkColor));
    }

    addInput(model) {
        return super.addPort(new BaseInputModel(model));
    }

    addOutput(model) {
        return super.addPort(new BaseOutputModel(model));
    }

    deSerialize(other, engine: SRD.DiagramEngine) {
        super.deSerialize(other, engine);
        this.model = other.model;
    }

    serialize() {
        return _.merge(super.serialize(), {
            model: this.model
        });
    }

    getInvoke() {
        return _.find(this.ports, ["type", "invoke"]);
    }

    getDispatch() {
        return _.find(this.ports, ["type", "dispatch"]);
    }

    getInputs() {
        return _.filter(this.ports, ["type", "input"]);
    }

    getOutputs() {
        return _.filter(this.ports, ["type", "output"]);
    }
}