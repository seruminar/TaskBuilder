import * as _ from "lodash";

import { InvokeModel } from "./InvokeModel";
import { BasePortModel } from "./BasePortModel";
import { IDispatchModel } from "../../../models/function/IDispatchModel";
import { CallerLinkModel } from "../link/CallerLinkModel";
import { INamedPortModel } from "../../../models/function/INamedPortModel";

export class DispatchModel extends BasePortModel<IDispatchModel> {
    constructor(name: string = "") {
        super(name, "dispatch");
    }

    get model() {
        const found = this.parentModel.getFunction().dispatchs.find(o => o.name === this.getName());

        if (found) {
            return found;
        }

        throw new Error(`Model ${this.getName()} not found!`);
    }

    canLinkToPort(other: BasePortModel<INamedPortModel>) {
        if (other instanceof InvokeModel) {
            // If there is an existing link, remove it
            if (_.size(other.getLinks()) > 1) {
                other.getLinks()[Object.keys(other.getLinks())[0]].remove();
            }
            else if (_.size(this.getLinks()) > 1) {
                this.getLinks()[Object.keys(this.getLinks())[0]].remove();
            }

            return true;
        }

        return false;
    }

    createLinkModel() {
        return new CallerLinkModel(this.model.name.toLowerCase(), this.parentModel.getFunction().displayColor);
    }
}