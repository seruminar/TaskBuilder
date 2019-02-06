class BasePortFactory extends SRD.AbstractPortFactory {
    getNewInstance() {
        switch (this.type) {
            case "invoke":
                return new BaseInvokeModel();
            case "dispatch":
                return new BaseDispatchModel();
            case "input":
                return new BaseInputModel();
            case "output":
                return new BaseOutputModel();
        }
    }
}