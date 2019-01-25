const SRD = window["storm-react-diagrams"];

class BaseInvokeModel extends SRD.PortModel {
    constructor(name) {
        super(name, "Invoke");
    }

    linked = _.size(this.links) !== 0;

    canLinkToPort(other) {
        //return other instanceof BaseDispatchModel;
        return false;
    }

    createLinkModel() {
        return new BaseCallerLinkModel(this.getName(), this.getParent().getFunction().displayColor);
    }
}