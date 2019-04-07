import * as _ from "lodash";

import { InputModel } from "./InputModel";
import { BasePortModel } from "./BasePortModel";
import { IOutputModel } from "../../../models/function/IOutputModel";
import { IParameterModel } from "../../../models/function/IParameterModel";
import { ParameterLinkModel } from "../link/ParameterLinkModel";

export class OutputModel extends BasePortModel<IOutputModel> {
    constructor(name: string = "") {
        super(name, "output");
    }

    get model() {
        const found = this.parentModel.getFunction().outputs.find(o => o.name === this.getName());

        if (found) {
            return found;
        }

        throw new Error(`Model ${this.getName()} not found!`);
    }

    canLinkToPort(other: BasePortModel<IParameterModel>) {
        if (other instanceof InputModel
            && this.model.typeNames.indexOf(other.model.typeNames[0]) > -1
        ) {
            // If there is an existing link, remove it.
            if (_.size(other.getLinks()) > 1) {
                other.getLinks()[Object.keys(other.getLinks())[0]].remove();
            }
            return true;
        }

        return false;
    }

    createLinkModel() {
        return new ParameterLinkModel("parameter", this.model.displayColor);
    }
}