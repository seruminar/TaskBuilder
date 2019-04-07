import * as _ from "lodash";
import { PortModel } from "storm-react-diagrams";

import { FunctionModel } from "../FunctionModel";

export abstract class BasePortModel<TModel = {}> extends PortModel {
    abstract get model(): TModel;

    isLinked = () => _.size(this.links) !== 0;

    get parentModel(): FunctionModel {
        return super.getParent() as FunctionModel;
    }

    set parentModel(parent: FunctionModel) {
        super.setParent(parent);
    }
}