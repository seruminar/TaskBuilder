const SRD = window["storm-react-diagrams"];

class BaseFunctionModel extends SRD.NodeModel {
    model = null;

    constructor(model, forcePorts) {
        super("function");
        this.model = model;

        if (forcePorts) {
            this.addInvoke(model.invoke, model.displayColor);

            if (model.dispatchs.length) {
                model.dispatchs.map(d => this.addDispatch(d, model.displayColor));
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

    deSerialize(other, engine) {
        super.deSerialize(other, engine);
        this.model = other.function;
    }

    serialize() {
        return _.merge(super.serialize(), {
            function: this.model
        });
    }

    getInvoke() {
        return _.find(this.ports, ["type", "invoke"]);
    }

    getDispatchs() {
        return _.filter(this.ports, ["type", "dispatch"]);
    }

    getInputs() {
        return _.filter(this.ports, ["type", "input"]);
    }

    getOutputs() {
        return _.filter(this.ports, ["type", "output"]);
    }
}