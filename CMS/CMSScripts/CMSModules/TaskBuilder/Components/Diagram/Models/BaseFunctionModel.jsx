const SRD = window["storm-react-diagrams"];

class BaseFunctionModel extends SRD.NodeModel {
    function = null;

    constructor(func, forcePorts) {
        super("function");
        this.function = func;

        if (forcePorts) {
            this.addInvoke(this.function.invoke);
            this.function.dispatchs.map(d => this.addDispatch(d));
            this.function.inputs.map(i => this.addInput(i));
            this.function.outputs.map(o => this.addOutput(o));
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

    getFunction() {
        return this.function;
    }

    getInvoke() {
        return _.find(this.ports, p => p.type === "Invoke");
    }

    getDispatchs() {
        return _.filter(this.ports, p => p.type === "Dispatch");
    }

    getInputs() {
        return _.filter(this.ports, p => p.type === "Input");
    }

    getOutputs() {
        return _.filter(this.ports, p => p.type === "Output");
    }

    setLocked(locked) {
        super.setLocked(locked);
        _.forEach(this.ports, port => {
            _.forEach(port.getLinks(), link => {
                link.setLocked(locked);
            });
        });
    }
}