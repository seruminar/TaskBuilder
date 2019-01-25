const SRD = window["storm-react-diagrams"];

class BasePortFactory extends SRD.AbstractPortFactory {
    getNewInstance(initialConfig) {
        switch (this.type) {
            case "Invoke":
                return new BaseInvokeModel(null, null);
            case "Dispatch":
                return new BaseDispatchModel(null, null);
            case "Input":
                return new BaseInputModel(null);
            case "Output":
                return new BaseOutputModel(null);
        }
    }
}