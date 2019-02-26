class BaseInvokeModel extends SRD.PortModel {
    constructor(name) {
        super(name, "invoke");
    }

    isLinked = () => _.size(this.links) !== 0;

    createLinkModel() {
        return new BaseCallerLinkModel(this.getName().toLowerCase(), "#FFFFFF");
    }
}