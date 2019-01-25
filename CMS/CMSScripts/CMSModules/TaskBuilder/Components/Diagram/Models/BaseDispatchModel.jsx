const SRD = window["storm-react-diagrams"];

class BaseDispatchModel extends SRD.PortModel {
    constructor(name) {
        super(name, "Dispatch");
    }

    getModel() {
        return this.getParent().getFunction().dispatchs.find(o => o.name === this.getName());
    }

    linked = _.size(this.links) !== 0;

    canLinkToPort(other) {
        return other instanceof BaseInvokeModel;
    }

    createLinkModel() {
        return new BaseCallerLinkModel(this.getModel().name, this.getParent().getFunction().displayColor);
    }
}