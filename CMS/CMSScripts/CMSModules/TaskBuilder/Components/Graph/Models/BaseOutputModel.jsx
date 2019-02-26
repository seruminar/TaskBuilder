class BaseOutputModel extends SRD.PortModel {
    constructor(name) {
        super(name, "output");
    }

    getModel() {
        return this.getParent().getFunction().outputs.find(o => o.name === this.getName());
    }

    isLinked = () => _.size(this.links) !== 0;

    canLinkToPort(other) {
        if (other instanceof BaseInputModel
            && this.getModel().typeNames.indexOf(other.getModel().typeNames[0]) > -1
        ) {
            // If there is an existing link, remove it.
            if (_.size(other.getLinks()) > 1) {
                other.getLinks()[Object.keys(other.getLinks())[0]].remove();
            }
            return true;
        }

        return false;
    }

    createLinkModel() {
        return new BaseParameterLinkModel("parameter", this.getModel().displayColor);
    }
}