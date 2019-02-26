class BaseInputModel extends SRD.PortModel {
    constructor(name) {
        super(name, "input");
    }

    getModel() {
        return this.getParent().getFunction().inputs.find(o => o.name === this.getName());
    }

    isLinked = () => _.size(this.getLinks()) !== 0;

    addLink(link) {
        super.addLink(link);

        taskBuilderDataSource.removeInputValue(this.getID());
    }

    removeLink(link) {
        super.removeLink(link);

        taskBuilderDataSource.setInputValue(this.getID(), this.getModel().filledModel);
    }

    createLinkModel() {
        return new BaseParameterLinkModel("parameter", this.getModel().displayColor);
    }
}