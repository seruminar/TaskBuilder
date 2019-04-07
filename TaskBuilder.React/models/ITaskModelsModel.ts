import { IFunctionModel } from "./function/IFunctionModel";

export interface ITaskModelsModel {
    functions: IFunctionModel[];
    authorizedFunctionGuids: string[];
    ports: string[];
    links: string[];
}