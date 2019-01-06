const SRD = window["storm-react-diagrams"];

class BasePortFactory extends SRD.AbstractPortFactory {
    constructor(type: string) {
        super(type);
    }

    getNewInstance(initialConfig?: any): BasePortModel {
        return new BasePortModel();
    }
}