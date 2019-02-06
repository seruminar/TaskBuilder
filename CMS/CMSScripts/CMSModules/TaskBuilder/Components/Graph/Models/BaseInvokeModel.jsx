class BaseInvokeModel extends SRD.PortModel {
    constructor(name) {
        super(name, "invoke");
    }

    isLinked = () => _.size(this.links) !== 0;

    canLinkToPort(other) {
        //return other instanceof BaseDispatchModel;
        return false;
    }

    createLinkModel() {
        return new BaseCallerLinkModel(this.getName().toLowerCase(), this.getParent().getFunction().displayColor);
    }
}