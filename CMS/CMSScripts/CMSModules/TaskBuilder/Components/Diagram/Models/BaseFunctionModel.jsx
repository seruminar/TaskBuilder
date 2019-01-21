const SRD = window["storm-react-diagrams"];

class BaseFunctionModel extends SRD.NodeModel {
    function = null;

    constructor(func, forcePorts) {
        super("function");
        this.function = func;

        if (forcePorts) {
            this.addInvoke(this.function.invoke);

            if (this.function.dispatchs.length) {
                this.function.dispatchs.map(d => this.addDispatch(d));
            }

            if (this.function.inputs.length) {
                this.function.inputs.map(i => this.addInput(i));
            }

            if (this.function.outputs.length) {
                this.function.outputs.map(o => this.addOutput(o));
            }
        }
    }

    addInvoke(model) {
        return super.addPort(new BaseInvokeModel(model.name));
    }

    addDispatch(model) {
        return super.addPort(new BaseDispatchModel(model.name));
    }

    addInput(model) {
        return super.addPort(new BaseInputModel(model.name));
    }

    addOutput(model) {
        return super.addPort(new BaseOutputModel(model.name));
    }

    deSerialize(other, engine) {
        super.deSerialize(other, engine);
        this.function = other.function;
    }

    serialize() {
        return _.merge(super.serialize(), {
            function: this.function
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