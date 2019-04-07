import { BasePortModel } from "./BasePortModel";
import { LinkModel } from "storm-react-diagrams";
import { IInputModel } from "../../../models/function/IInputModel";
import { taskBuilderDataSource } from "../../../taskBuilder/TaskBuilderDataSource";
import { ParameterLinkModel } from "../link/ParameterLinkModel";

export class InputModel extends BasePortModel<IInputModel>  {
    constructor(name: string = "") {
        super(name, "input");
    }

    get model() {
        const found = this.parentModel.getFunction().inputs.find(o => o.name === this.getName());

        if (found) {
            return found;
        }

        throw new Error(`Model ${this.getName()} not found!`);
    }

    addLink(link: LinkModel) {
        super.addLink(link);

        taskBuilderDataSource.removeInputValue(this.getID());
    }

    removeLink(link: LinkModel) {
        super.removeLink(link);

        taskBuilderDataSource.setInputValue(this.getID(), this.model.filledModel);
    }

    createLinkModel() {
        return new ParameterLinkModel("parameter", this.model.displayColor);
    }
}