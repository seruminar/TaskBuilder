class BaseDispatchModel extends SRD.PortModel {
    constructor(name) {
        super(name, "dispatch");
    }

    getModel() {
        return this.getParent().getFunction().dispatchs.find(o => o.name === this.getName());
    }

    isLinked = () => _.size(this.links) !== 0;

    canLinkToPort(other) {
        if (other instanceof BaseInvokeModel) {
            // If there is an existing link, remove it
            if (_.size(other.getLinks()) > 1) {
                other.getLinks()[Object.keys(other.getLinks())[0]].remove();
            }
            else if (_.size(this.getLinks()) > 1) {
                this.getLinks()[Object.keys(this.getLinks())[0]].remove();
            }

            return true;
        }

        return false;
    }

    createLinkModel() {
        return new BaseCallerLinkModel(this.getModel().name.toLowerCase(), this.getParent().getFunction().displayColor);
    }
}