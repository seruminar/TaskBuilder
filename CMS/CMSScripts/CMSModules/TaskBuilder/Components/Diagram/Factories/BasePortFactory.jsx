const SRD = window["storm-react-diagrams"];

class BasePortFactory extends SRD.AbstractPortFactory {
    getNewInstance(initialConfig) {
        switch (this.type) {
            case "invoke":
                return new BaseInvokeModel(null, null);
            case "dispatch":
                return new BaseDispatchModel(null, null);
            case "input":
                return new BaseInputModel(null);
            case "output":
                return new BaseOutputModel(null);
        }
    }
}