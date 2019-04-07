import * as _ from "lodash";

import { ITaskBuilderProps } from "./TaskBuilder";
import { ITaskModelsModel } from "../models/ITaskModelsModel";
import { ITaskGraphModel } from "../models/ITaskGraphModel";
import { IInputValueModel } from "../models/function/inputValue/IInputValueModel";
import { IFunctionModel } from "../models/function/IFunctionModel";

export class TaskBuilderDataSource {
    models: ITaskModelsModel;
    graph: ITaskGraphModel;
    endpoints: { [endpoint: string]: string };
    secureToken: string;

    load(props: ITaskBuilderProps) {
        this.models = props.models;
        this.graph = props.graph;
        this.endpoints = props.endpoints;
        this.secureToken = props.secureToken;

        if (!this.graph.graph.inputValues) {
            this.graph.graph.inputValues = {};
        }
    }

    getAuthorizedFunctions() {
        return _.intersectionWith(
            this.models.functions,
            this.models.authorizedFunctionGuids,
            (a, b) => a.typeGuid === b
        );
    }

    getFunctionByTypeGuid(typeGuid: string): IFunctionModel {
        const found = _.find(this.models.functions, f => f.typeGuid === typeGuid);

        if (found) {
            return found;
        }

        throw new Error(`Function ${typeGuid} not found!`);
    }

    getInputValue(inputID: string) {
        return this.graph.graph.inputValues[inputID];
    }

    setInputValue(inputID: string, inputValue: IInputValueModel) {
        this.graph.graph.inputValues[inputID] = JSON.parse(JSON.stringify(inputValue));
    }

    removeInputValue(inputID: string) {
        delete this.graph.graph.inputValues[inputID];
    }
}

export const taskBuilderDataSource = new TaskBuilderDataSource();