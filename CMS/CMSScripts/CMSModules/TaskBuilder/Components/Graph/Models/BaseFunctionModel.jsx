class BaseFunctionModel extends SRD.NodeModel {
    functionTypeGuid = null;

    constructor(func, locationPoint) {
        super("function");

        this.functionTypeGuid = func.typeGuid;

        if (locationPoint) {
            this.addInvoke(func.invoke);
            func.dispatchs.map(d => this.addDispatch(d));
            func.inputs.map(i => this.addInput(i));
            func.outputs.map(o => this.addOutput(o));

            this.x = locationPoint.x;
            this.y = locationPoint.y;
        }
    }

    addInvoke(model) {
        return super.addPort(new BaseInvokeModel(model.name));
    }

    addDispatch(model) {
        return super.addPort(new BaseDispatchModel(model.name));
    }

    addInput(model) {
        const port = super.addPort(new BaseInputModel(model.name));

        if (model.inlineOnly) {
            port.setLocked(true);
        }

        return port;
    }

    addOutput(model) {
        const port = super.addPort(new BaseOutputModel(model.name));

        if (model.inlineOnly) {
            port.setLocked(true);
        }

        return port;
    }

    deSerialize(other, engine) {
        super.deSerialize(other, engine);
        this.functionTypeGuid = other.functionTypeGuid;
    }

    serialize() {
        return _.merge(super.serialize(), {
            functionTypeGuid: this.functionTypeGuid
        });
    }

    getFunction() {
        return taskBuilderDataSource
            .getFunctionByTypeGuid(this.functionTypeGuid);
    }

    getInvoke() {
        return _.find(this.ports, p => p.type === "invoke");
    }

    getDispatchs() {
        return _.filter(this.ports, p => p.type === "dispatch");
    }

    getInputs() {
        return _.filter(this.ports, p => p.type === "input");
    }

    getOutputs() {
        return _.filter(this.ports, p => p.type === "output");
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