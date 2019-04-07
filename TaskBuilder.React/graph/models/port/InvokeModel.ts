import { BasePortModel } from "./BasePortModel";
import { IInvokeModel } from "../../../models/function/IInvokeModel";
import { CallerLinkModel } from "../link/CallerLinkModel";

export class InvokeModel extends BasePortModel<IInvokeModel> {
    constructor(name: string = "") {
        super(name, "invoke");
    }

    model: IInvokeModel;

    createLinkModel() {
        return new CallerLinkModel(this.getName().toLowerCase(), "#FFFFFF");
    }
}